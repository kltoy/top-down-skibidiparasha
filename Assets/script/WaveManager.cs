using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public GameMode[] Mode;
    
    public Transform[] spawnpoints;

    public Transform playerTransform;

    public TMP_Text killsText;
    public TMP_Text wavecountText;
    public float delayUntilNextWave;
    public GameObject panelWin;
    public GameObject prefabEnemy;
    public TMP_Text bestwavenumber;

    [HideInInspector]
    public int wavenumber = 0;

    private int countKills = 0;
    private int maxEnemyInGame;

    private GameModeSelect GameModeSelect;
    private GameMode ActiveMode;

    public GameMode GetActiveMode()
    {
        return ActiveMode;
    }

    void Start()
    {
        GameModeSelect = FindFirstObjectByType<GameModeSelect>();

        Mode[0].mode = GameMode.GameModes.Easy;
        Mode[1].mode = GameMode.GameModes.Normal;
        Mode[2].mode = GameMode.GameModes.Hard;

        if (GameModeSelect != null )
        {
            if (GameModeSelect.gameMode == "Easy")
            {
                ActiveMode = Mode[0];
                bestwavenumber.text  =  YG.YandexGame.savesData.easyrecords.ToString();
            }

            if (GameModeSelect.gameMode == "Medium")
            {
                ActiveMode = Mode[1];
                bestwavenumber.text = YG.YandexGame.savesData.normalrecords.ToString();
            }

            if (GameModeSelect.gameMode == "Hard")
            {
                ActiveMode = Mode[2];
                bestwavenumber.text = YG.YandexGame.savesData.hardrecords.ToString();
            }
        }
        else
            ActiveMode = Mode[0];

        print("Активный гейммод: " + ActiveMode.mode);
        WaveSpawner();
    }

    private void Update()
    {
        wavecountText.text = (wavenumber + 1).ToString();
    }

    public void DeadSkibi()
    {
        countKills++;
        killsText.text = countKills.ToString();
    }

    public void WaveSpawner()
    {
        StartCoroutine(EnemySpawn(ActiveMode.currentInfo));
        maxEnemyInGame += ActiveMode.currentInfo.count;
        StartCoroutine(WaitEndWave());
    }

    IEnumerator WaitEndWave()
    {
        while (countKills < maxEnemyInGame)
        {
            yield return null;
        }

        wavenumber++;

        yield return new WaitForSeconds(delayUntilNextWave);
        WaveSpawner();
    }

    private Transform GetRandomPoint()
    {
        int randomnumberp = Random.Range(0, spawnpoints.Length); //рандомный спавнпоинт
        Transform randomspawnpoint = spawnpoints[randomnumberp];
        float distance = (playerTransform.position - randomspawnpoint.position).magnitude;

        if (distance > 3)
        {
            return randomspawnpoint;
        }
        else
        {
            return GetRandomPoint();
        }
    }

    IEnumerator EnemySpawn(EnemyInfo enemyInfo) 
    {
        int count = enemyInfo.count;

        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = GetRandomPoint();

            GameObject enemy = Instantiate(prefabEnemy, spawnPoint.position, prefabEnemy.transform.rotation); //sam spawn
            SkibidiController skibidi_Controller = enemy.GetComponent<SkibidiController>();

            skibidi_Controller.targetTransform = playerTransform; //стартовые настройки 
            skibidi_Controller.speed = enemyInfo.speed;
            skibidi_Controller.damage = enemyInfo.damage;
            skibidi_Controller.health = enemyInfo.health;

            yield return new WaitForSeconds(enemyInfo.spawndelay);
        }

        ActiveMode.currentInfo.damage += ActiveMode.rostAttributes.damage;
        ActiveMode.currentInfo.speed += ActiveMode.rostAttributes.speed;
        ActiveMode.currentInfo.health += ActiveMode.rostAttributes.health;
        ActiveMode.currentInfo.count += ActiveMode.rostAttributes.count;
        ActiveMode.currentInfo.spawndelay += ActiveMode.rostAttributes.spawndelay;
    }
}

[System.Serializable]
public class GameMode
{
    public enum GameModes 
    { 
        Easy,
        Normal,
        Hard
    }

    public GameModes mode;

    public EnemyInfo currentInfo;
    public EnemyInfo rostAttributes;
}

[System.Serializable]
public class EnemyInfo
{
    public float damage = 5;
    public float speed = 1;
    public float health = 100;
    public int count = 2;
    public float spawndelay;
}