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

    void Upgrade(Stat<float> stat) // TODO give feedback on operation success
    {
        int upgradeCost = UpgradeSystem.GetSpeedUpgradeCost(stat);
        if (upgradeCost <= playerMoney.coins)
        {
            playerMoney.coins -= upgradeCost;
            stat.currentUpgradeLevel++; // TODO handle limits
            stat.value += 5; // TODO remove
        }
    }
}
