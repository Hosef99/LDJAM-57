using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UndergroundUI : MonoBehaviour{
    private PlayerUpgrade playerUpgrade;
    private PlayerData playerData;
    private int abilityNo = 0;
    public GameObject shopPanel;
    public TextMeshProUGUI staminaValue;
    public TextMeshProUGUI levelValue;
    public TextMeshProUGUI goldValue;
    public TextMeshProUGUI diamondValue;
    public TextMeshProUGUI redstoneValue;
    public Image[] abilityIcons;
    public TextMeshProUGUI[] titles;
    public TextMeshProUGUI[] descriptions;
    public TextMeshProUGUI[] values;
    public Button[] buybuttons;
    public string[] slotIDs;
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
        playerData = FindObjectOfType<PlayerData>();
        playerUpgrade = FindObjectOfType<PlayerUpgrade>();

        goldValue.text = playerData.goldCount.ToString();
        diamondValue.text = playerData.diamondCount.ToString();
        redstoneValue.text = playerData.redStoneCount.ToString();
        if (playerData.cardSlots > 3){
            for (int i = 0; i < 6-playerData.cardSlots+1; i++)
            {
                slots[i].SetActive(true);
            }
        }
    }

    public void UpdateCards(List<UpgradeData> upgradeDatas)
    {
        // Filter out active upgrades
        List<UpgradeData> inactiveUpgrades = upgradeDatas.FindAll(upg => !upg.isActive);

        // Pick 3 unique upgrades from the inactive ones
        List<UpgradeData> selectedCards = GetRandomUniqueElements(inactiveUpgrades, 3);

        for (int i = 0; i < selectedCards.Count; i++)
        {
            slotIDs[i] = selectedCards[i].upgradeID;
            abilityIcons[i].sprite = selectedCards[i].icon;
            titles[i].text = selectedCards[i].displayName;
            descriptions[i].text = selectedCards[i].description;
            values[i].text = selectedCards[i].levels[0].cost.ToString();
        }
        
        for (int i = selectedCards.Count; i < 3; i++)
        {
            buybuttons[i].gameObject.SetActive(false);
        }
    }


    public void BuyCard(int slotID)
    {
        if (abilityNo == playerData.cardSlots)
        {
            return;
        }

        int cost = playerUpgrade.GetUpgrade(slotIDs[slotID]).levels[0].cost;

        if (playerData.redStoneCount >= cost){
            playerData.redStoneCount -= cost;
            playerData.Upgrade(slotIDs[slotID]);
            UpdateUI();
            slotIcons[abilityNo].gameObject.SetActive(true);
            slotIcons[abilityNo].sprite = playerUpgrade.GetUpgrade(slotIDs[slotID]).icon;
            abilityNo++;
            int index = playerUpgrade.GetUpgradeIndex(slotIDs[slotID]);
            playerUpgrade.upgrades[index].isActive = true;

            titles[slotID].text = "Sold Out";
            descriptions[slotID].text = "";
        }else{
            Debug.Log("Not enough redstone");
        }
    }

    public void ShowShop()
    {
        UpdateCards(playerUpgrade.upgrades);
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
