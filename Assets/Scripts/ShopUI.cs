// New cleaner version of ShopUI.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Currency Displays")]
    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI goldText;

    [Header("Stat Displays")]
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI visionText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI slotText;

    [Header("Upgrade Buttons")]
    public TextMeshProUGUI[] powerUpText;
    public TextMeshProUGUI[] value;
    public Image[] coins;
    public Image[] resourceImage;

    public ResourceIcon resourceIcon;

    private PlayerData data;
    private UpgradeManager upgManager;

    private UpgradeID[] upgradeIDs = new UpgradeID[] {
        UpgradeID.PermStamina,
        UpgradeID.PermVision,
        UpgradeID.PermBomb,
        UpgradeID.PermSlot
    };

    private string[] statNames = new string[] {
        "Stamina", "Vision Range", "Bomb", "Slot Size"
    };

    void Start()
    {
        data = PlayerData.Instance;
        upgManager = UpgradeManager.Instance;
        UpdateButtons();
        UpdateUI(); 
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        staminaText.text = data.GetStatValue(UpgradeID.PermStamina).ToString();
        visionText.text = data.GetStatValue(UpgradeID.PermVision).ToString();
        bombText.text = data.GetStatValue(UpgradeID.PermBomb).ToString();
        slotText.text = data.GetStatValue(UpgradeID.PermSlot).ToString();
        diamondText.text = data.GetStatValue(Stat.Diamond).ToString();
        goldText.text = data.GetStatValue(Stat.Gold).ToString();
    }

    void UpdateButtons()
    {
        for (int i = 0; i < upgradeIDs.Length; i++)
        {
            Upgrade upg = upgManager.GetPermanentUpgrade(upgradeIDs[i]);
            if (upg is StatsUpgrade statsUpgrade)
            {
                int currentLevel = UpgradeManager.Instance.GetUpgradeLevel(statsUpgrade.upgradeID);

                if (currentLevel < statsUpgrade.upgradeLevels.Count)
                {
                    UpgradeLevel levelInfo = statsUpgrade.upgradeLevels[currentLevel];
                    float val = levelInfo.value;
                    int cost = statsUpgrade.upgradeLevels[currentLevel].cost;

                    powerUpText[i].text = $"+{val} {statNames[i]}";
                    value[i].text = cost.ToString();
                    coins[i].enabled = true;
                    resourceImage[i].sprite = resourceIcon.GetIcon(levelInfo.costType);
                }
                else
                {
                    powerUpText[i].text = "MAX";
                    value[i].text = "";
                    coins[i].enabled = false;
                }
            }
        }
    }

    public void UpgradeStat(int index)
    {
        if (index < 0 || index >= upgradeIDs.Length) return;

        if (upgManager.GetPermanentUpgrade(upgradeIDs[index]) is StatsUpgrade statsUpgrade)
        {
            int currentLevel = UpgradeManager.Instance.upgradeLevels[statsUpgrade.upgradeID];
            if (currentLevel >= statsUpgrade.upgradeLevels.Count) return;
            Debug.Log("Test");
            UpgradeLevel level = statsUpgrade.upgradeLevels[currentLevel];
            if (data.GetStatValue(level.costType) >= level.cost)
            {
                statsUpgrade.DoUpgrade();
                data.AddStat(level.costType, -level.cost);
                SoundManager.Instance.PlaySFX("powerUp");
                UpdateButtons();
            }
            else
            {
                Debug.Log("Not enough resources");
            }
        }
    }
}
