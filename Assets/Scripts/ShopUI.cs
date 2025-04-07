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

    void UpgradeStamina()
    {
        UpgradeData upgrade = playerUpgrade.GetPermanentUpgrade("stamina");
        if(upgrade.IsMaxed()){
            return;
        }
        int upgradeCost = upgrade.currentCost;
        if (playerData.goldCount >= upgradeCost)
        {
            playerUpgrade.UpgradePermanent("stamina");
            playerData.goldCount -= upgradeCost;
            if (upgrade.IsMaxed())
            {
                powerUpText[0].text = "MAX";
                coins[0].enabled = false;
                value[0].text = "";
            }
            else{
                
            }
        }
    }

    void UpgradeVision()
    {

    }

    void UpgradeBomb()
    {

    }

    void UpgradeSlot()
    {

    }
}
