using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewStatsUpgrade", menuName = "Upgrades/StatsUpgrade")]
public class StatsUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        if (currentLevel >= upgradeLevels.Count)
        {
            Debug.LogWarning("Max upgrade level reached.");
            return;
        }

        var level = upgradeLevels[currentLevel];

        foreach (var item in level.upgradedStats)
        {
            PlayerData.Instance.AddStat(item.stat, item.value);
        }

        if (isTemp)
        {
            UpgradeManager.Instance.AddTempUpgrade(this);
        }

        currentLevel++;
    }
}