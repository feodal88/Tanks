using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] protected GameObject bulletSpawner;

    protected HealthBar healthBarScript;

    [SerializeField] protected int maxHealth;
    [SerializeField] protected float movementSpeed;
 
    public float projectileSpeed;
    public int damage;
    protected int health;
    protected bool isReloading = true; 

    protected void Init()
    {
        health = maxHealth;
        healthBarScript = GetComponentInChildren<HealthBar>();
        healthBarScript.Init(maxHealth);      
    }

    public virtual void TakeDamage(int dmg)
    {
        health-= dmg;
        if(health < 0)
            health = 0;

        healthBarScript.UpdateHealthBar(health);
        if (health <= 0)
        {
            if(gameObject.CompareTag("Enemy"))
                GameManager.Instanse.DecreaseEnemiesLeft();
            Destroy(gameObject);
        }
    }

    public void UpgradeMaxHP(int amount)
    {
        maxHealth += amount;
    }

    public void UpgradeDamage(int amount)
    {
        damage += amount;
    }

}

