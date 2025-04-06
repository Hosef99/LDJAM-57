using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;

public class ShopManager : MonoBehaviour{
    public enum ShopType{Permenant, Underground}
    public ShopUI shopUI;

    public void OpenShop(ShopType type){
        List<UpgradeData> upgrades;
        if(type == ShopType.Permenant){
            upgrades = new List<UpgradeData>{
                new UpgradeData{upgradeID = "vision", displayName ="Increase Vision", maxLevel = 5,baseCost = 100, costPerLevel = 50},
            };
        }
        else{
            upgrades = GetRandomUpgrades();
        }
        shopUI.OpenShop(upgrades);
    }

    List<UpgradeData> GetRandomUpgrades(){
        List<UpgradeData> allUpgrades = new List<UpgradeData>{
            new UpgradeData{upgradeID = "vision", displayName ="Increase Vision", maxLevel = 5,baseCost = 100, costPerLevel = 50},
        };

        List<UpgradeData> randomUpgrades = new List<UpgradeData>();
        for (int i = 0; i < 3; i ++){
            int randomIndex = Random.Range(0, allUpgrades.Count);
            randomUpgrades.Add(allUpgrades[randomIndex]);
            allUpgrades.RemoveAt(randomIndex); //not repeated
        }
        return randomUpgrades;
    }

}
