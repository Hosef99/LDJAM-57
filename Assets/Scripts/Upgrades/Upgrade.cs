using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeID
{
    BombImmune,
    BombCollectOre,
    HorizontalDig,
    VerticalDig,
    DuplicateOre,
    MasterYi,
    PermStamina,
    PermVision,
    PermBomb,
    PermSlot,
}

[System.Serializable]
public class UpgradedStat
{
    public Stat stat;
    public float value;
}

[System.Serializable]
public class UpgradeLevel
{
    public List<UpgradedStat> upgradedStats;
    public ResourceAmount cost;
}

public abstract class Upgrade : ScriptableObject 
{
    public Sprite icon;
    public UpgradeID upgradeID;
    public string upgradeName;
    public string description;
    public List<UpgradeLevel> upgradeLevels;
    public bool isTemp;
    public abstract void DoUpgrade();
}
