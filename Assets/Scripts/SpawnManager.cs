using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject t0Prefab, t1Prefab;
    [SerializeField] GameObject pickUpCoinPrefab, pickUpHealthPrefab;

    [SerializeField] float spawnEnemyDelay_start;
    float spawnEnemyDelay;
    float firstSpawnDelay = 0.5f;
    [SerializeField] float spawnPickUpDelayMin;
    [SerializeField] float spawnPickUpDelayMax;

    [SerializeField] bool canSpawnEnemy = true;
    bool canSpawnPickUp = true;

    float offsetX = 16;
    float positionZ = 0.8f;

    private void Start()
    {
        spawnEnemyDelay = spawnEnemyDelay_start;
    }

    void Update()
    {
        if (GameManager.Instanse.gameIsRunning)
        {
            if (canSpawnEnemy)
                StartCoroutine(SpawnEnemy());

            if (canSpawnPickUp)
                StartCoroutine(SpawnPickUp());
        }
    }

    IEnumerator SpawnPickUp()
    {
        canSpawnPickUp = false;
        float delay = Random.Range(spawnPickUpDelayMin, spawnPickUpDelayMax);
        yield return new WaitForSeconds(delay);

        if (GameManager.Instanse.gameIsRunning)
        {
            GameObject prefab = ChoosePickUpPrefabToCreate();
            Instantiate(prefab, GetRandomPosition(), prefab.transform.rotation);        
        }
        canSpawnPickUp = true;
    }

    IEnumerator SpawnEnemy()
    {
        canSpawnEnemy = false;

        float spawnDelay;
        if (firstSpawnDelay >= 0)
        {
            spawnDelay = firstSpawnDelay;
            firstSpawnDelay = -1;
        }
        else
            spawnDelay = spawnEnemyDelay;

        yield return new WaitForSeconds(spawnDelay);

        if (GameManager.Instanse.currentLevel.EnemiesLeft() != 0)
        {
            spawnEnemyDelay *= 0.98f;

            GameObject prefab = ÑhooseEnemyPrefabToCreate();
            Instantiate(prefab, GetRandomPosition(), prefab.transform.rotation);
        }
        else
            spawnEnemyDelay = spawnEnemyDelay_start;

        canSpawnEnemy = true;        
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-offsetX, offsetX), 0.25f, positionZ);
    }

    GameObject ÑhooseEnemyPrefabToCreate()
    {
        int enemyNumber = GameManager.Instanse.currentLevel.GetEnemyNumberToSpawn();
        switch(enemyNumber)
        {
            case 0:
                return t0Prefab;
            case 1:
                return t1Prefab;
            default:
                return null;
        }
    }

    GameObject ChoosePickUpPrefabToCreate()
    {
        float hpPercent = GameObject.Find("Player").GetComponent<PlayerTank>().NeedInHPPickUp();

        if (Random.Range(0, 1f) <= hpPercent)
            return pickUpHealthPrefab;
        else
            return pickUpCoinPrefab;
    }
}
