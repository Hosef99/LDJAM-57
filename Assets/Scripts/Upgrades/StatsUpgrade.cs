using System.Collections.Generic;

[System.Serializable]
public class UpgradedStat
{
    public Stat stat;
    public float value;
}

public class StatsUpgrade : Upgrade
{
    public List<UpgradedStat> appliedUpgrade = new();

    public override void DoUpgrade()
    {
        foreach (var item in appliedUpgrade)
        {
            PlayerData.Instance.AddStat(item.stat, item.value);
        }
    }
}