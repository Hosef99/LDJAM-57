using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradedStat
{
    public Stat stat;
    public float value;
}

[CreateAssetMenu(fileName = "NewStatsUpgrade", menuName = "Upgrades/StatsUpgrade")]
public class StatsUpgrade : Upgrade
{
    public List<List<UpgradedStat>> statLevel = new();
    public int currentLevel;
    public override void DoUpgrade()
    {
        if (currentLevel >= statLevel.Count)
        {
            Debug.LogWarning("Max upgrade level reached.");
            return;
        }

        var level = statLevel[currentLevel];

        foreach (var item in level)
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