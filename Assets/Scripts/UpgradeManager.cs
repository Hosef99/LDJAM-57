using System.Collections.Generic;
using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour{
    public static UpgradeManager Instance;
    public Upgrade[] permanentUpgrades;
    public Upgrade[] tempUpgrades;
    public List<Upgrade> activeUpgrades = new();
    public Dictionary<UpgradeID, int> upgradeLevels = new();

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
            upgradeLevels[upgrade.upgradeID] = 0;
        }
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

    public void AddTempUpgrade(Upgrade upg){
        activeUpgrades.Add(upg);
    }

    public void ApplyTempUpgrade()
    {
        foreach (var upg in tempUpgrades)
        {
            upg.DoUpgrade();
        }
    }

    public int GetUpgradeLevel(UpgradeID upgradeID)
    {
        if (upgradeLevels.ContainsKey(upgradeID))
        {
            return upgradeLevels[upgradeID];
        }
        upgradeLevels[upgradeID] = 0;
        return 0;
    }

    public int SetUpgradeLevel(UpgradeID upgradeID, int level)
    {
        return 0;
    }
}
