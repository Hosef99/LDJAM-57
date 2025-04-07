using UnityEngine;
[System.Serializable]
public class UpgradeData
{
    public string upgradeID;
    public string displayName;
    public int level = 0;

    public Level[] levels;

    // for underground upgrades
    
    public bool isActive;
    public string description;

    public bool IsMaxed(){
        return level >= levels.Length;
    }
}
