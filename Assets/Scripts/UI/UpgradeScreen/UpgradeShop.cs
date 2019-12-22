using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI evolvePriceText;
    public long blobID;
    private void Awake()
    {
        //playerMoney = gameObject.GetComponent<PlayerMoney>();
        //blobStats = blob.GetComponent<BlobStats>();
        //PrintUpgradePrices(); // TODO remove
        SaveSystem.MakeFirstSave();
        //SaveSystem.Save(); // TODO remove
        SaveData saveData = SaveSystem.Load();

        moneyText.text = saveData.money.ToString();
        PrintUpgradePrices();
        //GameObject go = BlobInstantiator.GetBlobGameObject(blobStats); // TODO remove
        //Instantiate(go, new Vector3(0.0F, 0.0F, 0.0F), Quaternion.identity); // TODO remove
    }
    void PrintUpgradePrices()
    {
        SaveData saveData = SaveSystem.Load();
        
        int speedCost = UpgradeSystem.GetSpeedUpgradeCost(SaveDataUtility.GetBlobStats(saveData, blobID).Speed);
        SaveDataUtility.GetBlobStats(saveData, blobID).Speed.value = 3.0F;
        Debug.Log(speedCost);
    }

    bool Upgrade(Stat stat)
    {
        int upgradeCost = UpgradeSystem.GetSpeedUpgradeCost(stat);
        if (CanUpgrade(stat, upgradeCost))
        {
            SaveDataUtility.PayMoney(upgradeCost);
            return true;
        }
        return false;
    }

    bool CanUpgrade(Stat stat, int upgradeCost)
    {
        if (upgradeCost <= SaveDataUtility.GetMoney()) 
        {
            if (stat.CanUpgrade())
            {
                return true;
            }
        }
        return false;
    }
}
