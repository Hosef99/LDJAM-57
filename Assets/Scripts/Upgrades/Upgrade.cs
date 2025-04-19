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
}

public abstract class Upgrade : ScriptableObject 
{
    public Sprite icon;
    public UpgradeID upgradeID;
    public string upgradeName;
    public string description;
    public ResourceAmount cost;
    public bool isTemp;
    public abstract void DoUpgrade();
}
