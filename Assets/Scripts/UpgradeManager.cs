using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    PlayerTank player;

    void Start()
    {
        instance = this;
        player = GameObject.Find("Player").GetComponent<PlayerTank>(); 
    }

    public void PurchaseUpgrade(Upgrade upgrade)
    {
        switch(upgrade.upgradeType)
        {
            case UpgradeType.ProjectilesCount:
                UpgradePlayerProjectilesCount(upgrade.Value);
                break;
            case UpgradeType.ProjectileSpeed:
                UpgradePlayerProjectileSpeed(upgrade.Value);
                break;
            case UpgradeType.PlayerHP:
                UpgradePlayerHP(upgrade.Value);
                break;
            case UpgradeType.PlayerDmg:
                UpgradePlayerDamage(upgrade.Value);
                break;
            case UpgradeType.AllyCounts:
                UpgradeAllyCount(upgrade.Value);
                break;
            case UpgradeType.AllyAS:
                UpgradeAllyAttackSpeed(upgrade.Value);
                break;
            case UpgradeType.AllyHP:
                UpgradeAllyHP(upgrade.Value);
                break;
            case UpgradeType.AllyDmg:
                UpgradeAllyDamage(upgrade.Value);
                break;
            case UpgradeType.MoreGold:
                UpgradeGoldFromCoins(upgrade.Value);
                break;
            case UpgradeType.Mines:
                UpgradeMines(upgrade.Value);
                break;
            case UpgradeType.SlowEnemy:
                UpgradeSlowEnemy(upgrade.Value);
                break;
            default:
                break;
        }
    }

    public void UpdateAllUpgrades()
    {
        Upgrade[] upgrades = FindObjectsOfType<Upgrade>(true);
        foreach(Upgrade upgrade in upgrades)
            upgrade.UpdateDisplay();
    }

    public void UpgradePlayerProjectilesCount(int amount)
    {
        player.maxBullets += amount;
        GameManager.Instanse.UpdateProjectilesCount();
    }

    public void UpgradePlayerProjectileSpeed(int percent)
    {
        player.projectileSpeed *= (1 + percent / 100);
    }

    public void UpgradePlayerHP(int value)
    {
        player.UpgradeMaxHP(value);
    }

    public void UpgradePlayerDamage(int value)
    {
        player.UpgradeDamage(value);
    }

    public void UpgradeAllyCount(int value)
    {
        AllyUtility.instance.count += value;   
    }

    public void UpgradeAllyAttackSpeed(int value)
    {
        AllyUtility.instance.ASReduceKoef *= 1 - value/100f;
    }

    public void UpgradeAllyHP(int value)
    {
        AllyUtility.instance.bonusHP += value;
    }

    public void UpgradeAllyDamage(int value)
    {
        AllyUtility.instance.bonusDmg += value;
    }

    public void UpgradeGoldFromCoins(int value)
    {
        GameManager.Instanse.bonusGoldIncome += value;
    }

    public void UpgradeMines(int value)
    {

    }

    public void UpgradeSlowEnemy(float value)
    {

    }
}

public enum UpgradeType
{
    ProjectilesCount, ProjectileSpeed, PlayerHP, PlayerDmg, AllyCounts, AllyAS, AllyHP, AllyDmg, MoreGold, Mines, SlowEnemy
}
