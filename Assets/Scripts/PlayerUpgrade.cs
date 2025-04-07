using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour{
    public List<UpgradeData> permanentUpgrades;
    public List<UpgradeData> upgrades = new List<UpgradeData>();

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

    public UpgradeData GetUpgrade(string id){
        return upgrades.Find(upg => upg.upgradeID == id);
    }

    public int GetUpgradeIndex(string id)
    {
        return upgrades.FindIndex(upg => upg.upgradeID == id);
    }

    public void ResetUpgrades()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].isActive = false;
        }
    }

}
