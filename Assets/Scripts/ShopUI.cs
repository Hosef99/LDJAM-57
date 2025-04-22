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
        staminaText.text = data.GetStatValue(Stat.MaxStamina).ToString();
        visionText.text = data.GetStatValue(Stat.Vision).ToString();
        bombText.text = data.GetStatValue(Stat.MaxBomb).ToString();
        slotText.text = data.GetStatValue(Stat.TempUpgradeSlots).ToString();
        diamondText.text = data.GetStatValue(Stat.Diamond).ToString();
        goldText.text = data.GetStatValue(Stat.Gold).ToString();
    }

    void UpdateButtons()
    {
        for (int i = 0; i < upgradeIDs.Length; i++)
        {
            if (upgManager.GetPermanentUpgrade(upgradeIDs[i]) is StatsUpgrade statsUpgrade)
            {
                int currentLevel = statsUpgrade.currentLevel;

                if (currentLevel < statsUpgrade.upgradeLevels.Count)
                {
                    float val = statsUpgrade.upgradeLevels[currentLevel].upgradedStats[0].value;
                    int cost = (int)statsUpgrade.upgradeLevels[currentLevel].cost.value;

                    powerUpText[i].text = $"+{val} {statNames[i]}";
                    value[i].text = cost.ToString();
                    coins[i].enabled = true;
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
            int currentLevel = statsUpgrade.currentLevel;
            if (currentLevel >= statsUpgrade.upgradeLevels.Count) return;

            ResourceAmount cost = statsUpgrade.upgradeLevels[currentLevel].cost;
            if (data.GetStatValue(cost.GetResourceTypeInStat()) >= cost.value)
            {
                statsUpgrade.DoUpgrade();
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
