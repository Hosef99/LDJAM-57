using UnityEngine;

public class UpgradeData : MonoBehaviour{
    public string upgradeID;
    public string displayName;
    public int level = 0;
    public int maxLevel = 5;
    public int baseCost = 50;
    public int costPerLevel = 25;
    public int CurrentCost => baseCost + level * costPerLevel;

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
