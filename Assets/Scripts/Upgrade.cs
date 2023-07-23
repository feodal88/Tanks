using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Upgrade : MonoBehaviour
{
    [SerializeField] int maxLvl;
    [SerializeField] int startCost;
    [SerializeField] int costIncrement;

    public int CurrentLvl { get; set; }
    public int CurrentCost { get; set; }
    public int Value;

    public UpgradeType upgradeType;

    [SerializeField] TextMeshProUGUI costTMP;
    [SerializeField] TextMeshProUGUI levelTMP;
    [SerializeField] Button btn;

    void Awake()
    {
        if (CurrentCost == 0)
            CurrentCost = startCost;
    }

    public void Purchase()
    {
        GameManager.Instanse.UpdateCoinsCount(-CurrentCost);

        CurrentLvl++;
        CurrentCost = startCost + costIncrement * CurrentLvl;

        UpgradeManager.instance.PurchaseUpgrade(this);
        UpgradeManager.instance.UpdateAllUpgrades();
    }

    public void UpdateDisplay()
    {
        costTMP.text = $"Cost: {CurrentCost}";
        levelTMP.text = $"{CurrentLvl} / {maxLvl}";
        btn.GetComponentInChildren<TextMeshProUGUI>().text = GetButtonName();

        if (CurrentLvl >= maxLvl)
        {
            costTMP.text = "";
            btn.interactable = false;
        }
        else
        {
            if (GameManager.Instanse.coins >= CurrentCost)
            {
                costTMP.color = Color.green;
                btn.interactable = true;
            }
            else
            {
                costTMP.color = Color.red;
                btn.interactable = false;
            }
        }
    }

    private void OnEnable()
    {
        UpdateDisplay();
    }

    string GetButtonName()
    {
        string type;
        switch (upgradeType)
        {
            case UpgradeType.PlayerHP:
                type = $"+{Value} HP";
                break;
            case UpgradeType.ProjectileSpeed:
                type = $"+{Value}% Projectile speed";
                break;
            case UpgradeType.ProjectilesCount:
                type = $"+{Value} Projectile";
                break;
            case UpgradeType.PlayerDmg:
                type = $"+{Value} Damage";
                break;
            case UpgradeType.AllyCounts:
                type = $"+{Value} Ally";
                break;
            case UpgradeType.AllyAS:
                type = $"+{Value}% Attack speed";
                break;
            case UpgradeType.AllyDmg:
                type = $"+{Value} Damage";
                break;
            case UpgradeType.AllyHP:
                type = $"+{Value} HP";
                break;
            case UpgradeType.MoreGold:
                type = $"+{Value} Gold from coin";
                break;
            case UpgradeType.Mines:
                type = $"+{Value} Mines";
                break;
            case UpgradeType.SlowEnemy:
                type = $"+{Value}% Slow enemies";
                break;
            default:
                type = "";
                break;
        }

        return type;
    }

}
