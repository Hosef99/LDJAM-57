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

public class PlayerData : MonoBehaviour{
    public static PlayerData Instance;
    public Dictionary<Stat, float> stats = new();
    
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

    public void InitializeStats()
    {
        stats[Stat.MaxStamina] = 100;
        stats[Stat.CurrentStamina] = 100;
        stats[Stat.Vision] = 3;
        stats[Stat.MaxBomb] = 3;
        stats[Stat.CurrBomb] = 3;
        stats[Stat.TempUpgradeSlots] = 3;
        stats[Stat.Gold] = 0;
        stats[Stat.Redstone] = 0;
        stats[Stat.Diamond] = 0;
        
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
