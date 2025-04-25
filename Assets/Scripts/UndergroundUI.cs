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

        goldValue.text = ((int)data.GetStatValue(Stat.Gold)).ToString();
        diamondValue.text = ((int)data.GetStatValue(Stat.Diamond)).ToString();
        redstoneValue.text = ((int)data.GetStatValue(Stat.Redstone)).ToString();
        bombValue.text = ((int)data.GetStatValue(Stat.CurrBomb)).ToString();
        if (data.GetStatValue(UpgradeStat.TempUpgradeSlots) > 3){
            for (int i = 0; i < data.GetStatValue(UpgradeStat.TempUpgradeSlots)-3; i++)
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

    public void UpdateCards()
    {
        Upgrade upg;

        List<Upgrade> selectedUpgrades = UpgradeManager.Instance.tempUpgrades
    .Where(upg => upg is StatsUpgrade statsUpgrade && UpgradeManager.Instance.GetUpgradeLevel(statsUpgrade.upgradeID) >= statsUpgrade.upgradeLevels.Count)
    .Cast<Upgrade>()
    .ToList();

        List<Upgrade> availableTempUpgrades = GetRandomUniqueElements(selectedUpgrades, 3);


        for (int i = 0; i < availableTempUpgrades.Count; i++)
        {
            upg = availableTempUpgrades[i];
            int currentLevel = UpgradeManager.Instance.GetUpgradeLevel(upg.upgradeID);
            slotIDs[i] = availableTempUpgrades[i];
            abilityIcons[i].sprite = upg.icon;
            titles[i].text = upg.upgradeName;
            descriptions[i].text = upg.description;
            values[i].text = upg.upgradeLevels[currentLevel].cost.value.ToString();
            resourceIcons[i].sprite = resourceIcon.GetIcon(upg.upgradeLevels[currentLevel].cost.resourceType);

        }
        
        for (int i = availableTempUpgrades.Count; i < 3; i++)
        {
            buybuttons[i].gameObject.SetActive(false);
        }
    }


    public void BuyCard(int slotID)
    {
        if (abilityNo == data.GetStatValue(UpgradeStat.TempUpgradeSlots))
        {
            return;
        }

        Upgrade upg = slotIDs[slotID];
        int currentLevel = UpgradeManager.Instance.GetUpgradeLevel(upg.upgradeID);

        int cost = (int)upg.upgradeLevels[currentLevel].cost.value;

        if (data.GetStatValue(Stat.Redstone) >= cost){
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
        UpdateCards();
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
