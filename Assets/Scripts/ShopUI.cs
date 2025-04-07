using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Currency Displays")]
    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI redstoneText;

    [Header("Stat Displays")]
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI visionText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI slotText;

    [Header("Upgrade Buttons")]
    public TextMeshProUGUI[] powerUpText;
    public TextMeshProUGUI[] value;
    public Image[] coins;

    private PlayerData playerData;
    private PlayerUpgrade playerUpgrade;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        playerUpgrade = FindObjectOfType<PlayerUpgrade>();
        UpgradeData upg = playerUpgrade.GetPermanentUpgrade("stamina");
        powerUpText[0].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Stamina";
        value[0].text = upg.levels[upg.level].cost.ToString();
        upg = playerUpgrade.GetPermanentUpgrade("vision");
        powerUpText[1].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Vision Range";
        value[1].text = upg.levels[upg.level].cost.ToString();
        upg = playerUpgrade.GetPermanentUpgrade("bomb");
        powerUpText[2].text = "+" + upg.levels[upg.level].addedValue.ToString() + " BOMBA";
        value[2].text = upg.levels[upg.level].cost.ToString();
        upg = playerUpgrade.GetPermanentUpgrade("slot");
        powerUpText[3].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Slot Size";
        value[3].text = upg.levels[upg.level].cost.ToString();
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        staminaText.text = playerData.stamina.ToString();
        visionText.text = playerData.vision.ToString();
        bombText.text = playerData.bombCount.ToString();
        slotText.text = playerData.cardSlots.ToString();
        diamondText.text = playerData.diamondCount.ToString();
        coinText.text = playerData.goldCount.ToString();
        redstoneText.text = playerData.redStoneCount.ToString();
        
        
    }

    public void UpgradeStamina()
    {
        UpgradeData upg = playerUpgrade.GetPermanentUpgrade("stamina");
        if(upg.IsMaxed()){
            return;
        }
        int upgradeCost = upg.levels[upg.level].cost;
        if (playerData.goldCount >= upgradeCost)
        {
            playerData.stamina += upg.levels[upg.level].addedValue;
            playerUpgrade.GetPermanentUpgrade("stamina").level++;
            playerData.goldCount -= upgradeCost;
            if (upg.IsMaxed())
            {
                powerUpText[0].text = "MAX";
                coins[0].enabled = false;
                value[0].text = "";
            }
            else
            {
                powerUpText[0].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Stamina";
                value[0].text = upg.levels[upg.level].cost.ToString();
            }
        }
        else
        {
            // not enough gold
        }
    }

    public void UpgradeVision()
    {
        UpgradeData upg = playerUpgrade.GetPermanentUpgrade("vision");
        if(upg.IsMaxed()){
            return;
        }
        int upgradeCost = upg.levels[upg.level].cost;
        if (playerData.goldCount >= upgradeCost)
        {
            playerData.vision += upg.levels[upg.level].addedValue;
            playerUpgrade.GetPermanentUpgrade("vision").level++;
            playerData.goldCount -= upgradeCost;
            if (upg.IsMaxed())
            {
                powerUpText[1].text = "MAX";
                coins[1].enabled = false;
                value[1].text = "";
            }
            else
            {
                powerUpText[1].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Vision";
                value[1].text = upg.levels[upg.level].cost.ToString();
            }
        }
        else
        {
            // not enough gold
        }
    }

    public void UpgradeBomb()
    {
        UpgradeData upg = playerUpgrade.GetPermanentUpgrade("bomb");
        if(upg.IsMaxed()){
            return;
        }
        int upgradeCost = upg.levels[upg.level].cost;
        if (playerData.goldCount >= upgradeCost)
        {
            playerData.bombCount += upg.levels[upg.level].addedValue;
            playerUpgrade.GetPermanentUpgrade("bomb").level++;
            playerData.goldCount -= upgradeCost;
            if (upg.IsMaxed())
            {
                powerUpText[2].text = "MAX";
                coins[2].enabled = false;
                value[2].text = "";
            }
            else
            {
                powerUpText[2].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Bomb";
                value[2].text = upg.levels[upg.level].cost.ToString();
            }
        }
        else
        {
            // not enough gold
        }
    }

    public void UpgradeSlot()
    {
        UpgradeData upg = playerUpgrade.GetPermanentUpgrade("slot");
        if(upg.IsMaxed()){
            return;
        }
        int upgradeCost = upg.levels[upg.level].cost;
        if (playerData.diamondCount >= upgradeCost)
        {
            playerData.cardSlots += upg.levels[upg.level].addedValue;
            playerUpgrade.GetPermanentUpgrade("slot").level++;
            playerData.diamondCount -= upgradeCost;
            if (upg.IsMaxed())
            {
                powerUpText[3].text = "MAX";
                coins[3].enabled = false;
                value[3].text = "";
            }
            else
            {
                powerUpText[3].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Slot Size";
                value[3].text = upg.levels[upg.level].cost.ToString();
            }
        }
        else
        {
            // not enough diamond
        }
    }
}
