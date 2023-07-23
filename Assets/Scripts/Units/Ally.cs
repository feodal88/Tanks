using UnityEngine;
using System.Collections;

public class Ally : Tank
{
    GameObject target;
    [SerializeField] GameObject cannon;
    [SerializeField] float reloadingTime = 4f;

    void Start()
    {
        Init();
        isReloading = false;
    }

    void Update()
    {
        FindTarget();
        Fire();
    }

    void Fire()
    {
        if (!isReloading && target != null)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledAllyProjectile();
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.transform.position = bulletSpawner.transform.position;
                ProjectileAlly projectileScript = bullet.GetComponent<ProjectileAlly>();
                projectileScript.target = target;
                projectileScript.speed = projectileSpeed;
                projectileScript.owner = this;
            }

            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadingTime);
        isReloading = false;
    }

    void FindTarget()
    {
        if (target == null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
                target = enemies[Random.Range(0, enemies.Length)];
        }
        else
            cannon.transform.LookAt(target.transform.position);
    }

    public void UpgradeAttackSpeed(float value)
    {
        reloadingTime *= value;
    }
}

