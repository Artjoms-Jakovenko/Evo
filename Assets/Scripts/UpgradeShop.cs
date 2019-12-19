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
        PrintUpgradePrices(); // TODO remove
        SaveSystem.Save(); // TODO remove
        GameObject go = BlobInstantiator.GetBlobGameObject(blobStats); // TODO remove
        Instantiate(go, new Vector3(0.0F, 0.0F, 0.0F), Quaternion.identity); // TODO remove
    }
    void PrintUpgradePrices()
    {
        int speedCost = UpgradeSystem.GetSpeedUpgradeCost(blobStats.stats.Speed);
        Debug.Log(speedCost);
        Upgrade(blobStats.stats.Speed);
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
