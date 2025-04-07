using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour{
    public List<UpgradeData> permanentUpgrades;
    public List<UpgradeData> upgradePool = new List<UpgradeData>();

    // upgrade the permenant upgrade by searching the ID
    public UpgradeData GetPermanentUpgrade(string id){
        return permanentUpgrades.Find(upg => upg.upgradeID == id);
    }

    //if not max level, upgrade it
    public bool UpgradePermanent(string id){
        UpgradeData upg = GetPermanentUpgrade(id);
        if (upg !=null && !upg.IsMaxed()) {
            upg.level++;
            return true;
        }
        return false;
    }

    //underground shop
    public bool UpgradeFromShop(UpgradeData shopData){
        UpgradeData upg = GetPermanentUpgrade(shopData.upgradeID);
        if(upg == null){

            upg = new UpgradeData()
            {
                upgradeID = shopData.upgradeID,
                displayName = shopData.displayName,
                level = 1,
                maxLevel = shopData.maxLevel,
                baseCost = shopData.baseCost,
                costPerLevel = shopData.costPerLevel
            };
            permanentUpgrades.Add(upg);
            return true;
        }else if(!upg.IsMaxed()){
            upg.level++;
            return true;
        }
        return false;
    }
}
