using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UndergroundUI : MonoBehaviour{

    private PlayerData playerData;
    public GameObject shopPanel;
    public TextMeshProUGUI staminaValue;
    public TextMeshProUGUI goldValue;
    public TextMeshProUGUI diamondValue;
    public TextMeshProUGUI redstoneValue;
    public Image[] abilityIcons;
    public TextMeshProUGUI[] titles;
    public TextMeshProUGUI[] descriptions;

    public string[] slotIDs;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    public void UpdateStamina(int stamina){
        staminaValue.text = stamina.ToString();
    }

    public void UpdateUI(){
        goldValue.text = playerData.goldCount.ToString();
        diamondValue.text = playerData.diamondCount.ToString();
        redstoneValue.text = playerData.redStoneCount.ToString();
    }

    public void UpdateCards(List<UpgradeData> upgradeDatas)
    {
        List<UpgradeData> selectedCards = GetRandomUniqueElements(upgradeDatas, 3);
        for (int i = 0; i < 0; i++)
        {
            slotIDs[i] = selectedCards[i].upgradeID;
            abilityIcons[i].sprite = selectedCards[i].icon;
            titles[i].text = selectedCards[i].displayName;
            descriptions[i].text = selectedCards[i].description;
        }
    }

    public void BuyCard(int slotID)
    {
        
        playerData.Upgrade(slotIDs[slotID]);
    }

    public void ShowShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
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
