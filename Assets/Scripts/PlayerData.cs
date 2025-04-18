using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum Stat
{
    maxStamina,
    currentStamina,
    vision,
    maxBomb,
    currBomb,
    tempUpgradeSlots,
    gold,
    redstone,
    diamond,
    bombImmune,
    bombCollectOre,
    horizontalDig,
    verticalDig,
    duplicateOre,
    masterYi,
    fossil1,
    fossil2,
    fossil3,
    fossil4,
    fossil5,
    fossil6
}

public class PlayerData : MonoBehaviour{
    public static PlayerData Instance;
    public Dictionary<Stat, float> stats = new();
    
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

    public float GetStat(Stat stat)
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
        stats[Stat.maxStamina] = 100;
        stats[Stat.currentStamina] = 100;
        stats[Stat.vision] = 3;
        stats[Stat.maxBomb] = 3;
        stats[Stat.currBomb] = 3;
        stats[Stat.tempUpgradeSlots] = 3;
        stats[Stat.gold] = 0;
        stats[Stat.redstone] = 0;
        stats[Stat.diamond] = 0;
        
        // Upgrades
        stats[Stat.bombImmune] = 0;
        stats[Stat.bombCollectOre] = 0;
        stats[Stat.horizontalDig] = 1;
        stats[Stat.verticalDig] = 1;
        stats[Stat.duplicateOre] = 0;
        stats[Stat.masterYi] = 0;

        // Fossils
        stats[Stat.fossil1] = 0;
        stats[Stat.fossil2] = 0;
        stats[Stat.fossil3] = 0;
        stats[Stat.fossil4] = 0;
        stats[Stat.fossil5] = 0;
        stats[Stat.fossil6] = 0;
    }
}
