using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Currency Displays")]
    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI coinText;

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
        UpdateButtons();
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
    }

    void UpdateButtons(){
        UpgradeData upg = playerUpgrade.GetPermanentUpgrade("stamina");
        if(upg.level != upg.levels.Length){
            powerUpText[0].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Stamina";
            value[0].text = upg.levels[upg.level].cost.ToString();
        }
        else{
            powerUpText[0].text = "MAX";
            coins[0].enabled = false;
            value[0].text = "";
        }
        upg = playerUpgrade.GetPermanentUpgrade("vision");
        if(upg.level != upg.levels.Length){
            powerUpText[1].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Vision Range";
            value[1].text = upg.levels[upg.level].cost.ToString();
        }
        else{
            powerUpText[1].text = "MAX";
            coins[1].enabled = false;
            value[1].text = "";
        }
        upg = playerUpgrade.GetPermanentUpgrade("bomb");
        if(upg.level != upg.levels.Length){
            powerUpText[2].text = "+" + upg.levels[upg.level].addedValue.ToString() + " BOMBA";
            value[2].text = upg.levels[upg.level].cost.ToString();
        }
        else{
            powerUpText[2].text = "MAX";
            coins[2].enabled = false;
            value[2].text = "";
        }
        upg = playerUpgrade.GetPermanentUpgrade("slot");
        if(upg.level != upg.levels.Length){
            powerUpText[3].text = "+" + upg.levels[upg.level].addedValue.ToString() + " Slot Size";
            value[3].text = upg.levels[upg.level].cost.ToString();
        }
        else{
            powerUpText[3].text = "MAX";
            coins[3].enabled = false;
            value[3].text = "";
        }
        
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
            SoundManager.Instance.PlaySFX("powerUp");
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
            SoundManager.Instance.PlaySFX("powerUp");

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
            SoundManager.Instance.PlaySFX("powerUp");

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
            SoundManager.Instance.PlaySFX("powerUp");
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
