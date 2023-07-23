using System.IO;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instanse;

    [SerializeField] GameObject playingInfo;
    [SerializeField] GameObject levelCompleteMenu;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject NextLevelBtn;
    [SerializeField] GameObject RestartLevelBtn;
    [SerializeField] GameObject[] projectileImages;

    [SerializeField] GameObject startPanel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI enemiesText;

    [HideInInspector] public Level currentLevel;
    PlayerTank player;

    int levelNumber = 0;
    int enemiesLeft;
    bool playerIsDead = false;
    public bool gameIsRunning;
    public int coins = 0;
    public int bonusGoldIncome = 0;

    private void Awake()
    {
        Instanse = this;

        player = GameObject.Find("Player").GetComponent<PlayerTank>();
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        StartLevel();
    }

    public void StartLevel()
    {
        levelNumber++;
        currentLevel = GameObject.Find("Level" + levelNumber).GetComponent<Level>();
        currentLevel.Init();

        playingInfo.SetActive(true);
        levelCompleteMenu.SetActive(false);

        gameIsRunning = true;

        SetLevelInfo();
        UpdateCoinsCount(0);

        player.StartLevel();
        AllyUtility.instance.StartLevel();
    }

    void SetLevelInfo()
    {
        levelText.text = "Level " + levelNumber;      
        enemiesLeft = currentLevel.EnemiesLeft();
        enemiesText.text = "Enemies: " + enemiesLeft;
    }

    public void GameOver()
    {
        gameIsRunning = false;
        playerIsDead = true;
        levelNumber--;

        Invoke("ShowLevelCompleteMenu", 2);
    }

    public void RestartLevel()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");

        foreach (GameObject enemy in enemies)
            Destroy(enemy);

        foreach (GameObject ally in allies)
            Destroy(ally);

        playerIsDead = false;
        StartLevel();
    }

    public void DecreaseEnemiesLeft()
    {
        enemiesLeft--;
        enemiesText.text = "Enemies: " + enemiesLeft;

        if (enemiesLeft <= 0)
            Invoke("ShowLevelCompleteMenu", 2);
    }

    void ShowLevelCompleteMenu()
    {
        gameIsRunning = false;
        levelCompleteMenu.SetActive(true);

        NextLevelBtn.SetActive(!playerIsDead);
        RestartLevelBtn.SetActive(playerIsDead);
    }

    public void UpdateCoinsCount(int changeAmount)
    {
        coins += changeAmount;
        coinsText.text = "Coins: " + coins;
    }

    public void UpdateProjectilesCount()
    {
        foreach(GameObject image in projectileImages)
        {
            if (System.Array.IndexOf(projectileImages, image) + 1 <= player.currentNumberOfBullets)
                image.SetActive(true);
            else
                image.SetActive(false);
        }
    }

    public void ShowHideUpgradeMenu()
    {
        upgradePanel.SetActive(!upgradePanel.activeInHierarchy);
        levelCompleteMenu.SetActive(!levelCompleteMenu.activeInHierarchy);
    }

    [System.Serializable]
    class SaveData
    {
        public int levelNumber;
        public int coins;
        public SaveUpgrade[] upgrades;

        public SaveData(int size)
        {
            upgrades = new SaveUpgrade[size];
        }

    }

    [System.Serializable]
    class SaveUpgrade
    {
        public UpgradeType upgradeType;
        public int level;
        public int cost;

        public SaveUpgrade(UpgradeType id, int level, int cost)
        {
            this.upgradeType = id;
            this.level = level;
            this.cost = cost;   
        }
    }

    void Save()
    {
        Upgrade[] upgrades = FindObjectsOfType<Upgrade>(true);

        SaveData saveData = new SaveData(upgrades.Length);
        saveData.levelNumber = levelNumber;
        saveData.coins = coins;

        for(int i = 0; i < upgrades.Length; i++)
        {
            saveData.upgrades[i] = new SaveUpgrade(upgrades[i].upgradeType, upgrades[i].CurrentLvl, upgrades[i].CurrentCost);
        }

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData savedata = JsonUtility.FromJson<SaveData>(json);

            levelNumber = savedata.levelNumber;
            coins = savedata.coins;

            Upgrade[] upgrades = FindObjectsOfType<Upgrade>(true);
            foreach (Upgrade upgrade in upgrades)
            {
                foreach (SaveUpgrade upgradeSaved in savedata.upgrades)
                {
                    if (upgrade.upgradeType == upgradeSaved.upgradeType)
                    {
                        upgrade.CurrentCost = upgradeSaved.cost;
                        upgrade.CurrentLvl = upgradeSaved.level;
                        for (int i = 0; i < upgrade.CurrentLvl; i++)
                            UpgradeManager.instance.PurchaseUpgrade(upgrade);
                        break;
                    }
                }
            }
        }
        StartGame();
    }

    public void SaveAndExit()
    {
        Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
       Application.Quit();   
#endif

    }
}
