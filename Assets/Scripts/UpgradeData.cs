using UnityEngine;
[System.Serializable]
public class UpgradeData
{
    public string upgradeID;
    public string displayName;
    public int level = 0;
    public int maxLevel = 5;

    public Level[] levels;



    public string GetDescription(){
        switch (upgradeID){
            case "vision":
                return "Increases vision range by " + (level + 1) + " units";
            default:
                return "unknown upgrade";
        }
    }

    public bool IsMaxed(){
        return level >= maxLevel;
    }
}
