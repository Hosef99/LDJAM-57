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
public class UpgradeLevel
{
    public int value;
    public Stat costType;
    public int cost;
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
