using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;

public class ShopManager : MonoBehaviour{
    public enum ShopType{Permenant, Underground}
    public ShopType shopType;
    public ShopUI shopUI;

    void Start () {
        if (shopType == ShopType.Permenant) {
            OpenPermenatShop();
        }
    }
    public void OpenPermenatShop(){
        List<UpgradeData> upgrades = new List<UpgradeData>{
            new UpgradeData{upgradeID = "vision", displayName ="Increase Vision", maxLevel = 5,baseCost = 100, costPerLevel = 50},
            new UpgradeData{upgradeID = "stamina", displayName ="Increase Stamina", maxLevel = 5,baseCost = 150, costPerLevel = 75},
        };

        shopUI.OpenShop(upgrades);
    }

    public void OpenUndergroundShop(){
         List<UpgradeData> allUpgrades = new List<UpgradeData>{
            new UpgradeData { upgradeID = "vision", displayName = "Increase Vision", maxLevel = 5, baseCost = 100, costPerLevel = 50},
            new UpgradeData { upgradeID = "stamina", displayName = "Increase Stamina", maxLevel = 5, baseCost = 150, costPerLevel = 75},
         };
         //randomize
        List<UpgradeData> randomUpgrades = new List<UpgradeData>();
        for (int i = 0; i < 3; i ++){
            int randomIndex = Random.Range(0, allUpgrades.Count);
            randomUpgrades.Add(allUpgrades[randomIndex]);
            allUpgrades.RemoveAt(randomIndex); //not repeated
        }

        shopUI.OpenShop(randomUpgrades);
    }
}
