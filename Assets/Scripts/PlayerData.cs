using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour{
    public static PlayerData Instance;

    public int gold = 0;
    public int stamina = 0;
    public int cardSlots = 5;

    public int bombCapcity = 1;
    public List<UpgradeData> upgrades = new List<UpgradeData>();

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
}
