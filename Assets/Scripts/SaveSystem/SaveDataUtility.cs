using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveDataUtility
{
    public static void PayMoney(SaveData saveData, int amount) // TODO get rid off?
    {
        saveData.inventory.AddToInventory(InventoryEnum.Money, -amount);
    }
}
