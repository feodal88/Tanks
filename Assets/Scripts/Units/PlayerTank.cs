using System.Collections;
using UnityEngine;

public class PlayerTank : Tank
{
    [SerializeField] public int maxBullets;
    public int currentNumberOfBullets;
    float invulnerabilityTime = 1f; 
    bool invulnerability = false;

    private void Start()
    {
        Init();
    }

    public void StartLevel()
    {
        currentNumberOfBullets = maxBullets;
        GameManager.Instanse.UpdateProjectilesCount();
        Init();
    }

    public void Fire()
    {
        if (currentNumberOfBullets > 0)
        {
            currentNumberOfBullets--;

            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledPlayerProjectile();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                pooledProjectile.transform.position = bulletSpawner.transform.position;
            }

            GameManager.Instanse.UpdateProjectilesCount();
        }
    }

    public override void TakeDamage(int dmg)
    {
        if (!invulnerability)
        {
            health-= dmg;
            healthBarScript.UpdateHealthBar(health);

            if (health == 0)
            {
                GameManager.Instanse.GameOver();
            }
            invulnerability = true;
            StartCoroutine(RestoreVulnerability());
        }
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void IncreaseCurrentNumberOfBullets()
    {
        currentNumberOfBullets++;
        GameManager.Instanse.UpdateProjectilesCount();
    }

    public float NeedInHPPickUp()
    {
        return 1 - (float)health / maxHealth;
    }

    IEnumerator RestoreVulnerability()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        invulnerability = false;
    }

}
