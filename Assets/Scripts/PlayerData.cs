using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum Stat
{
    CurrentStamina,
    CurrBomb,
    Gold,
    Redstone,
    Diamond,
    LapisLazuli,
    Fossil1,
    Fossil2,
    Fossil3,
    Fossil4,
    Fossil5,
    Fossil6
}

[System.Serializable]
public class StatData
{
    public Stat stat;
    public float value;
}

public class PlayerData : MonoBehaviour{
    public static PlayerData Instance;
    public Dictionary<Stat, float> stats = new();
    public Dictionary<UpgradeID, int> upgradeStats = new();

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            BaseStats();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetStatValue(Stat stat)
    {
        if (stats.TryGetValue(stat, out var value))
        {
            return value;
        }
        return 0;
    }

    public int GetStatValue(UpgradeID upgradeID)
    {
        if (upgradeStats.TryGetValue(upgradeID, out var value))
        {
            return value;
        }
        return 0;
    }

    public void SetStat(Stat stat, float value)
    {
        stats[stat] = value;
    }

    public void AddStat(Stat stat, float value)
    {
        if(!stats.ContainsKey(stat))
        {
            stats[stat] = 0f;
        }
        stats[stat] += value;
    }

    public void AddStat(UpgradeID stat, int value)
    {
        if(!upgradeStats.ContainsKey(stat))
        {
            upgradeStats[stat] = 0;
        }
        upgradeStats[stat] += value;
    }

    public void OnGame()
    {
        stats[Stat.CurrentStamina] = upgradeStats[UpgradeID.PermStamina];
        stats[Stat.CurrBomb] = upgradeStats[UpgradeID.PermBomb];
    }

    public void ResetUpgrades()
    {
        // Upgrades
        upgradeStats[UpgradeID.BombImmune] = 0;
        upgradeStats[UpgradeID.BombCollectOre] = 0;
        upgradeStats[UpgradeID.HorizontalDig] = 1;
        upgradeStats[UpgradeID.VerticalDig] = 1;
        upgradeStats[UpgradeID.DuplicateOre] = 0;
        upgradeStats[UpgradeID.MasterYi] = 0;
        stats[Stat.Redstone] = 0;
    }


    public void BaseStats()
    {
        // Resources
        stats[Stat.Gold] = 9999;
        stats[Stat.Redstone] = 0;
        stats[Stat.Diamond] = 100;
        stats[Stat.LapisLazuli] = 0;
        
        // Upgrades
        upgradeStats[UpgradeID.PermStamina] = 100;
        upgradeStats[UpgradeID.PermVision] = 0;
        upgradeStats[UpgradeID.PermBomb] = 3;
        upgradeStats[UpgradeID.PermSlot] = 3;
        upgradeStats[UpgradeID.BombImmune] = 0;
        upgradeStats[UpgradeID.BombCollectOre] = 0;
        upgradeStats[UpgradeID.HorizontalDig] = 1;
        upgradeStats[UpgradeID.VerticalDig] = 1;
        upgradeStats[UpgradeID.DuplicateOre] = 0;
        upgradeStats[UpgradeID.MasterYi] = 0;

        // Fossils
        stats[Stat.Fossil1] = 0;
        stats[Stat.Fossil2] = 0;
        stats[Stat.Fossil3] = 0;
        stats[Stat.Fossil4] = 0;
        stats[Stat.Fossil5] = 0;
        stats[Stat.Fossil6] = 0;
    }
}
