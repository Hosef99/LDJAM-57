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
}
