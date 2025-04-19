using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UndergroundUI : MonoBehaviour{
    public ResourceIcon resourceIcon;
    private UpgradeManager upgradeManager;
    private PlayerData data;
    private int abilityNo = 0;
    public GameObject shopPanel;
    public TextMeshProUGUI staminaValue;
    public TextMeshProUGUI levelValue;
    public TextMeshProUGUI goldValue;
    public TextMeshProUGUI diamondValue;
    public TextMeshProUGUI redstoneValue;
    public TextMeshProUGUI bombValue;

    // The card slots UI
    public Image[] abilityIcons;
    public TextMeshProUGUI[] titles;
    public TextMeshProUGUI[] descriptions;
    public TextMeshProUGUI[] values;
    public Image[] resourceIcons;
    public Button[] buybuttons;
    
    // To take note of the item in shop slot
    public Upgrade[] slotIDs;
    public GameObject[] slots;
    public Image[] slotIcons;

    void Start()
    {
        
    }

    public void UpdateStamina(int stamina){
        staminaValue.text = stamina.ToString();
    }

    public void UpdateLevel(int level){
        levelValue.text = level.ToString();
    }

    public void UpdateUI(){
        data = FindObjectOfType<PlayerData>();
        upgradeManager = UpgradeManager.Instance;

        goldValue.text = ((int)data.GetStat(Stat.Gold)).ToString();
        diamondValue.text = ((int)data.GetStat(Stat.Diamond)).ToString();
        redstoneValue.text = ((int)data.GetStat(Stat.Redstone)).ToString();
        bombValue.text = ((int)data.GetStat(Stat.CurrBomb)).ToString();
        if (data.GetStat(Stat.TempUpgradeSlots) > 3){
            for (int i = 0; i < data.GetStat(Stat.TempUpgradeSlots)-3; i++)
            {
                slots[i].SetActive(true);
            }
        }
    }

        public Upgrade[] GetAvailableTempUpgrades()
    {
        var activeIDs = upgradeManager.activeUpgrades.Select(upg => upg.upgradeID).ToHashSet();
        return upgradeManager.tempUpgrades.Where(upg => !activeIDs.Contains(upg.upgradeID)).ToArray();
    }

    public void UpdateCards(Upgrade[] availableTempUpgrades)
    {


        for (int i = 0; i < availableTempUpgrades.Length; i++)
        {
            slotIDs[i] = availableTempUpgrades[i];
            abilityIcons[i].sprite = availableTempUpgrades[i].icon;
            titles[i].text = availableTempUpgrades[i].upgradeName;
            descriptions[i].text = availableTempUpgrades[i].description;
            values[i].text = availableTempUpgrades[i].cost.value.ToString();
            resourceIcons[i].sprite = resourceIcon.GetIcon(availableTempUpgrades[i].cost.resourceType);

        }
        
        for (int i = availableTempUpgrades.Length; i < 3; i++)
        {
            buybuttons[i].gameObject.SetActive(false);
        }
    }


    public void BuyCard(int slotID)
    {
        if (abilityNo == data.GetStat(Stat.TempUpgradeSlots))
        {
            return;
        }

        Upgrade upg = slotIDs[slotID];
        int cost = (int)upg.cost.value;

        if (data.GetStat(Stat.Redstone) >= cost){
            data.AddStat(Stat.Redstone,-cost);
            upg.DoUpgrade();
            UpdateUI();
            slotIcons[abilityNo].gameObject.SetActive(true);
            slotIcons[abilityNo].sprite = upg.icon;
            abilityNo++;
            SoundManager.Instance.PlaySFX("powerUp");
            titles[slotID].text = "Sold Out";
            descriptions[slotID].text = "";
        }else{
            Debug.Log("Not enough redstone");
        }
    }

    public void ShowShop()
    {
        UpdateCards(GetAvailableTempUpgrades());
        shopPanel.SetActive(true);
    }

    List<T> GetRandomUniqueElements<T>(List<T> list, int count)
    {
        List<T> tempList = new List<T>(list);
        List<T> result = new List<T>();

        for (int i = 0; i < count && tempList.Count > 0; i++)
        {
            int randIndex = Random.Range(0, tempList.Count);
            result.Add(tempList[randIndex]);
            tempList.RemoveAt(randIndex);
        }

        return result;
    }


}
