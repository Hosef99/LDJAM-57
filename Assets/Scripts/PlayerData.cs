using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerData : MonoBehaviour{
    public static PlayerData Instance;
    public PlayerController playerController;
    public BoomScript boomScript;
    public Light2D light2D;
    public int goldCount = 0;
    public int fossil1Count = 0;
    public int fossil2Count = 0;
    public int fossil3Count = 0;
    public int fossil4Count = 0;
    public int fossil5Count = 0;
    public int fossil6Count = 0;
    public int sliverCount = 0;
    public int diamondCount = 0;
    public int redStoneCount = 0;
    public int stamina = 0;
    public int vision = 1;
    public int bombCount = 0;
    public int cardSlots = 5;

    public int bombCapcity = 1;
    public List<UpgradeData> upgrades;
    

    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public UpgradeData GetUpgrade(string id)
    {
        return upgrades.Find(u => u.upgradeID == id);
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        var existing = GetUpgrade(upgrade.upgradeID);
        if (existing != null)
        {
            existing.level = upgrade.level;
        }
        else
        {
            upgrades.Add(upgrade);
        }
    }

    public void Upgrade(string upgradeID){
        switch (upgradeID)
        {
            case "1":
            // other underground upgrades
            break;
            default:
            break;
        }
    }

    public void OnUpdate(){
        
    }


    public void VisionIncrease(int level)
    {
        switch (level)
        {
            case 0:
                light2D.falloffIntensity = 1;
                break;
            case 1:
                light2D.falloffIntensity = 0.8f;
                break;
            case 2:
                light2D.falloffIntensity = 0.65f;
                break;
            case 3:
                light2D.falloffIntensity = 0.5f;
                break;

        }
        
        
    }
    
    public void BoomRangeIncrease(int level)
    {
        switch (level)
        {
            case 0:
                boomScript.range = 2;

                break;
            case 1:
                boomScript.range = 4;
                break;
            case 2:
                boomScript.range = 6;
                break;
            case 3:
                boomScript.range = 8;
                break;

        }
        
        
    }

    public void HorizontalDigLevel(int level)
    {
        switch (level)
        {
            case 0:
                playerController.horizontalDigCount = 1;
                break;
            case 1:
                playerController.horizontalDigCount = 2;
                break;
            case 2:
                playerController.horizontalDigCount = 3;
                break;
            case 3:
                playerController.horizontalDigCount = 4;
                break;

        }
    }
    
    public void VerticalDigLevel(int level)
    {
        switch (level)
        {
            case 0:
                playerController.verticalDigCount = 1;
                break;
            case 1:
                playerController.verticalDigCount = 2;
                break;
            case 2:
                playerController.verticalDigCount = 3;
                break;
            case 3:
                playerController.verticalDigCount = 4;
                break;

        }
    }
    
    public void MasterYiLevel(int level)
    {
        playerController.masterYi = level;
    }
    
    public void OreLevel(int level)
    {
        switch (level)
        {
            case 0:
                playerController.oreBrokeChance = 1;
                break;
            case 1:
                playerController.oreBrokeChance = 0.9f;
                break;
            case 2:
                playerController.oreBrokeChance = 0.8f;
                break;
            case 3:
                playerController.oreBrokeChance = 0.7f;
                break;

        }
    }

    public void BoomCollect(int level)          // ---------------------------------------------------------------
    {
        
    }
    
    
    public void BoomImmune(int level)          // ---------------------------------------------------------------
    {
        
    }
    

}
