using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledProjectilesPlayer;
    public List<GameObject> pooledProjectilesEnemy;
    public List<GameObject> pooledProjectilesAlly;
    public GameObject projectilePlayerToPool;
    public GameObject projectileEnemyToPool;
    public GameObject projectileAllyToPool;
    public int playerAmountToPool, enemyAmountToPool, allyAmountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledProjectilesPlayer = new List<GameObject>();
        for (int i = 0; i < playerAmountToPool; i++)
        {
            GameObject obj = Instantiate(projectilePlayerToPool);
            obj.SetActive(false);
            pooledProjectilesPlayer.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }

        pooledProjectilesEnemy = new List<GameObject>();
        for (int i = 0; i < enemyAmountToPool; i++)
        {
            GameObject obj = Instantiate(projectileEnemyToPool);
            obj.SetActive(false);
            pooledProjectilesEnemy.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }

        pooledProjectilesAlly = new List<GameObject>();
        for (int i = 0; i < allyAmountToPool; i++)
        {
            GameObject obj = Instantiate(projectileAllyToPool);
            obj.SetActive(false);
            pooledProjectilesAlly.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }
    }

    public GameObject GetPooledPlayerProjectile()
    {
        return GetPooledProjectile(pooledProjectilesPlayer, projectilePlayerToPool);
    }

    public GameObject GetPooledEnemyProjectile()
    {
        return GetPooledProjectile(pooledProjectilesEnemy, projectileEnemyToPool);
    }

    public GameObject GetPooledAllyProjectile()
    {
        return GetPooledProjectile(pooledProjectilesAlly, projectileAllyToPool);
    }

    GameObject GetPooledProjectile(List<GameObject>  pooledList, GameObject prefabToCreate)
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledList.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledList[i].activeInHierarchy)
            {
                return pooledList[i];
            }
        }
        // otherwise, create
        GameObject obj = Instantiate(prefabToCreate);
        obj.SetActive(false);
        pooledList.Add(obj);
        obj.transform.SetParent(this.transform);
        return obj;
    }
}
