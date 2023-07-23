using System.Collections;
using UnityEngine;

public class Enemy : Tank
{ 
    bool isMoving = true;
    float stopPositionZ = -14;

    [SerializeField] float reloadingTimeMin = 2;
    [SerializeField] float reloadingTimeMax = 6;

    void Start() 
    {
        Init();    
        StartCoroutine(Reload());
    }

    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        if (GameManager.Instanse.gameIsRunning)
        {
            if (isMoving)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
                if (transform.position.z < stopPositionZ)
                    isMoving = false;
            }
            else
            {
                Transform playerPosition = GameObject.Find("Player").GetComponent<Transform>();
                transform.LookAt(playerPosition);
            }
        }
    }

    void Fire()
    {
        if (!isReloading && GameManager.Instanse.gameIsRunning)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledEnemyProjectile();
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.transform.position = bulletSpawner.transform.position;
                bullet.transform.rotation = transform.rotation;
                ProjectileEnemy bulletScript = bullet.GetComponent<ProjectileEnemy>();
                bulletScript.owner = this;
            }

            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        float reloadingTime = Random.Range(reloadingTimeMin, reloadingTimeMax);
        yield return new WaitForSeconds(reloadingTime);
        isReloading = false;
    }
}

