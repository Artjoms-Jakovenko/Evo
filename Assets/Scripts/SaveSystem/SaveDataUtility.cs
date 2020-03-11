using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveDataUtility
{
    public static void PayMoney(int amount) // TODO get rid off?
    {
        SaveSystem.saveData.inventory.AddToInventory(InventoryEnum.Money, -amount);
    }
}
