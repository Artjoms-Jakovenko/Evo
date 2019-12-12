using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    PlayerMoney playerMoney;
    public GameObject blob;
    BlobStats blobStats;
    private void Awake()
    {
        playerMoney = gameObject.GetComponent<PlayerMoney>();
        blobStats = blob.GetComponent<BlobStats>();
        PrintUpgradePrices();
    }
    void PrintUpgradePrices()
    {
        int speedCost = UpgradeSystem.GetSpeedUpgradeCost(blobStats.Speed);
        Debug.Log(speedCost);
        Upgrade(blobStats.Speed);
    }

    bool Upgrade(Stat stat)
    {
        int upgradeCost = UpgradeSystem.GetSpeedUpgradeCost(stat);
        if (CanUpgrade(stat, upgradeCost))
        {
            playerMoney.coins -= upgradeCost;
            return true;
        }
        return false;
    }

    bool CanUpgrade(Stat stat, int upgradeCost)
    {
        if (upgradeCost <= playerMoney.coins)
        {
            if (stat.CanUpgrade())
            {
                return true;
            }
        }
        return false;
    }
}
