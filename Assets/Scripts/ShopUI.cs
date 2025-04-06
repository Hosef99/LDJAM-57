using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ShopUI : MonoBehaviour{
    public GameObject shopPanel;
    public Transform upgradeListParent;
    public GameObject upgradeEntryPrefab;
    private List<UpgradeData> currentUpgrades;
    private PlayerUpgrade playerUpgrade;

    void Start()
    {
        playerUpgrade = FindObjectOfType<PlayerUpgrade>();
    }

    public void OpenShop(List<UpgradeData> upgrades){
        shopPanel.SetActive(true);
        currentUpgrades = upgrades;

        foreach (Transform child in upgradeListParent){
            Destroy(child.gameObject);
        }
        foreach(var upgrade in currentUpgrades ){
            GameObject entry = Instantiate(upgradeEntryPrefab,upgradeListParent);
            entry.transform.Find("Name").GetComponent<Text>().text = upgrade.displayName + " Lv. " + upgrade.level;
            entry.transform.Find("Description").GetComponent<Text>().text = upgrade.GetDescription();
            entry.transform.Find("Cost").GetComponent<Text>().text = "Cost: " + upgrade.CurrentCost;

            Button buyButton = entry.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => TryUpgrade(upgrade));
        }
    }

    public void TryUpgrade(UpgradeData upgrade){
        if (PlayerData.Instance.gold >= upgrade.CurrentCost && !upgrade.IsMaxed()){
            var existing = PlayerData.Instance.GetUpgrade(upgrade.upgradeID);
            if (existing != null){
                existing.level++;
            }
            else{
                upgrade.level = 1;
                PlayerData.Instance.upgrades.Add(upgrade);
            }
            Debug.Log("Upgraded: " + upgrade.displayName);
            OpenShop(currentUpgrades);
            //playerUpgrade.ApplyUpgrade(upgrade);
            //PlayerData.Instance.gold -= upgrade.CurrentCost;
        }else{
            Debug.Log("Not enough gold or upgrade is maxed");
        }
    }

    public void CloseShop(){
        shopPanel.SetActive(false);
    }
}
