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
    Fossil1,
    Fossil2,
    Fossil3,
    Fossil4,
    Fossil5,
    Fossil6
}

public enum UpgradeStat
{
    MaxStamina,
    Vision,
    MaxBomb,
    TempUpgradeSlots,
    BombImmune,
    BombCollectOre,
    HorizontalDig,
    VerticalDig,
    DuplicateOre,
    MasterYi,
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
    public Dictionary<UpgradeStat, int> upgradeStats = new();

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeStats();
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

    public int GetStatValue(UpgradeStat upgradeStat)
    {
        if (upgradeStats.TryGetValue(upgradeStat, out var value))
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

    public void OnGame()
    {
        stats[Stat.CurrentStamina] = upgradeStats[UpgradeStat.MaxStamina];
        stats[Stat.CurrBomb] = upgradeStats[UpgradeStat.MaxBomb];
    }

    public void ResetUpgrades()
    {
        // Upgrades
        upgradeStats[UpgradeStat.BombImmune] = 0;
        upgradeStats[UpgradeStat.BombCollectOre] = 0;
        upgradeStats[UpgradeStat.HorizontalDig] = 1;
        upgradeStats[UpgradeStat.VerticalDig] = 1;
        upgradeStats[UpgradeStat.DuplicateOre] = 0;
        upgradeStats[UpgradeStat.MasterYi] = 0;
        stats[Stat.Redstone] = 0;
    }


    public void InitializeStats()
    {
        // Resources
        stats[Stat.Gold] = 9999;
        stats[Stat.Redstone] = 0;
        stats[Stat.Diamond] = 100;
        
        // Upgrades
        upgradeStats[UpgradeStat.MaxStamina] = 100;
        upgradeStats[UpgradeStat.Vision] = 0;
        upgradeStats[UpgradeStat.MaxBomb] = 3;
        upgradeStats[UpgradeStat.TempUpgradeSlots] = 3;
        upgradeStats[UpgradeStat.BombImmune] = 0;
        upgradeStats[UpgradeStat.BombCollectOre] = 0;
        upgradeStats[UpgradeStat.HorizontalDig] = 1;
        upgradeStats[UpgradeStat.VerticalDig] = 1;
        upgradeStats[UpgradeStat.DuplicateOre] = 0;
        upgradeStats[UpgradeStat.MasterYi] = 0;

        // Fossils
        stats[Stat.Fossil1] = 0;
        stats[Stat.Fossil2] = 0;
        stats[Stat.Fossil3] = 0;
        stats[Stat.Fossil4] = 0;
        stats[Stat.Fossil5] = 0;
        stats[Stat.Fossil6] = 0;
    }
}
