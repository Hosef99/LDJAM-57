using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewStatsUpgrade", menuName = "Upgrades/StatsUpgrade")]
public class StatsUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        int currentLevel = UpgradeManager.Instance.upgradeLevels[upgradeID];
        if (currentLevel >= upgradeLevels.Count)
        {
            Debug.LogWarning("Max upgrade level reached.");
            return;
        }

        var level = upgradeLevels[currentLevel];

        
        PlayerData.Instance.AddStat(upgradeID, level.value);

        if (isTemp)
        {
            UpgradeManager.Instance.AddTempUpgrade(this);
        }
        UpgradeManager.Instance.upgradeLevels[upgradeID]++;
    }
}