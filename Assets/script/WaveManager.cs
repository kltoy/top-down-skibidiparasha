using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public WaveInfo[] wavesInfo;
    public Transform[] spawnpoints;

    public Transform playerTransform;

    public TMP_Text killsText;
    public TMP_Text wavecountText;
    public float delayUntilNextWave;

    [HideInInspector]
    public int wavenumber = 0;

    private int countKills = 0;
    private int maxEnemyInGame;

    void Start()
    {
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
        WaveInfo wave = wavesInfo[wavenumber];
        StartCoroutine(EnemySpawn(wave.enemyInfo));
        maxEnemyInGame += wave.enemyInfo.count;
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
        yield return new WaitForSeconds(enemyInfo.delayUntilSpawn); //zaderjka
        int count = enemyInfo.count;

        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = GetRandomPoint();

            GameObject enemy = Instantiate(enemyInfo.prefab, spawnPoint.position, enemyInfo.prefab.transform.rotation); //sam spawn
            SkibidiController skibidi_Controller = enemy.GetComponent<SkibidiController>();

            skibidi_Controller.targetTransform = playerTransform; //стартовые настройки 
            skibidi_Controller.speed = enemyInfo.speed;
            skibidi_Controller.damage = enemyInfo.damage;
            skibidi_Controller.health = enemyInfo.health;
            skibidi_Controller.rangeAttack = enemyInfo.rangeAttack;
            skibidi_Controller.fireRate = enemyInfo.FIRERATE;

            yield return new WaitForSeconds(enemyInfo.spawndelay);
        }
    }
}

[System.Serializable]
public class WaveInfo
{
    public EnemyInfo enemyInfo;
}
[System.Serializable]
public class EnemyInfo
{
    public GameObject prefab;
    public float delayUntilSpawn;
    public float damage = 5;
    public float speed = 1;
    public float health = 100;
    public int count = 2;
    public float spawndelay;
    public float rangeAttack;
    public float FIRERATE;
}