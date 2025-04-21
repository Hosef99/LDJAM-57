using System.Collections.Generic;
using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour{
    public static UpgradeManager Instance;
    public Upgrade[] permanentUpgrades;
    public Upgrade[] tempUpgrades;
    public List<Upgrade> activeUpgrades = new();

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
        permanentUpgrades = Resources.LoadAll<Upgrade>("Permanent/");
        tempUpgrades = Resources.LoadAll<Upgrade>("Temporary/");
    }

    // upgrade the permenant upgrade by searching the ID
    public Upgrade GetPermanentUpgrade(UpgradeID id){
        return Array.Find(permanentUpgrades, upg => upg.upgradeID == id);
    }

    public Upgrade GetTempUpgrade(UpgradeID id){
        return Array.Find(tempUpgrades, upg => upg.upgradeID == id);
    }

    public void ResetUpgrades()
    {
        activeUpgrades.Clear();
        foreach (var upgrade in tempUpgrades)
        {
            if (upgrade is StatsUpgrade statsUpgrade)
            {
                statsUpgrade.currentLevel = 0;
            }
        }
    }

    public void AddTempUpgrade(Upgrade upg){
        activeUpgrades.Add(upg);
    }
}
