using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum Stat
{
    MaxStamina,
    CurrentStamina,
    Vision,
    MaxBomb,
    CurrBomb,
    TempUpgradeSlots,
    Gold,
    Redstone,
    Diamond,
    BombImmune,
    BombCollectOre,
    HorizontalDig,
    VerticalDig,
    DuplicateOre,
    MasterYi,
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

    public Dictionary<UpgradeID, int> upgradeLevels = new();
    
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
        stats[Stat.CurrentStamina] = stats[Stat.MaxStamina];
        stats[Stat.CurrBomb] = stats[Stat.MaxBomb];
    }

    public void LevelUpgrade(Upgrade upgrade)
    {
        if (!upgradeLevels.ContainsKey(upgrade.upgradeID))
        {
            upgradeLevels[upgrade.upgradeID] = 1;
        }
        else if (upgradeLevels[upgrade.upgradeID] < upgrade.upgradeLevels.Count)
        {
            upgradeLevels[upgrade.upgradeID]++;
        }
    }

    public void InitializeStats()
    {
        stats[Stat.MaxStamina] = 10;
        stats[Stat.CurrentStamina] = 10;
        stats[Stat.Vision] = 0;
        stats[Stat.MaxBomb] = 3;
        stats[Stat.CurrBomb] = 3;
        stats[Stat.TempUpgradeSlots] = 3;
        stats[Stat.Gold] = 9999;
        stats[Stat.Redstone] = 0;
        stats[Stat.Diamond] = 100;
        
        // Upgrades
        stats[Stat.BombImmune] = 0;
        stats[Stat.BombCollectOre] = 0;
        stats[Stat.HorizontalDig] = 1;
        stats[Stat.VerticalDig] = 1;
        stats[Stat.DuplicateOre] = 0;
        stats[Stat.MasterYi] = 0;

        // Fossils
        stats[Stat.Fossil1] = 0;
        stats[Stat.Fossil2] = 0;
        stats[Stat.Fossil3] = 0;
        stats[Stat.Fossil4] = 0;
        stats[Stat.Fossil5] = 0;
        stats[Stat.Fossil6] = 0;
    }
}
