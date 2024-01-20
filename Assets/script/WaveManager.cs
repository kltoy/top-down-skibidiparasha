using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveInfo[] wavesInfo;

    public Transform[] spawnpoints;

    public Transform playerTransform;

    [HideInInspector]
    public int wavenumber = 0;

    void Start()
    {
        WaveSpawner();
    }

    public void WaveSpawner()
    {
        WaveInfo wave = wavesInfo[wavenumber];

        foreach (EnemyInfo enemyInfo in wave.enemy)
        {
            StartCoroutine(EnemySpawn(enemyInfo));
        }
    }

    IEnumerator EnemySpawn(EnemyInfo enemyInfo)
    {
        yield return new WaitForSeconds(enemyInfo.delayUntilSpawn); //zaderjka
        int count = enemyInfo.count;

        for (int i = 0; i < count; i++)
        {
            int randomnumberp = Random.Range(0, spawnpoints.Length); //рандомный спавнпоинт
            Transform randomspawnpoint = spawnpoints[randomnumberp];

            GameObject enemy = Instantiate(enemyInfo.prefab, randomspawnpoint.position, enemyInfo.prefab.transform.rotation); //sam spawn
            SkibidiController skibidi_Controller = enemy.GetComponent<SkibidiController>();

            skibidi_Controller.targetTransform = playerTransform; //стартовые настройки 
            skibidi_Controller.speed = enemyInfo.speed;
            skibidi_Controller.damage = enemyInfo.damage;
            skibidi_Controller.health = enemyInfo.health;
            skibidi_Controller.rangeAttack = enemyInfo.rangeAttack;

            yield return new WaitForSeconds(enemyInfo.spawndelay);
        }
    }
}

[System.Serializable]
public class WaveInfo
{
    public EnemyInfo[] enemy;
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
}