using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    [JsonProperty]
    private Dictionary<InventoryEnum, int> inventory = new Dictionary<InventoryEnum, int>();

    public void AddToInventory(InventoryEnum inventoryEnum, int amount)
    {
        if (inventory.ContainsKey(inventoryEnum))
        {
            inventory[inventoryEnum] += amount;
        }
        else
        {
            inventory.Add(inventoryEnum, amount);
        }
    }

    public int GetInventoryInfo(InventoryEnum inventoryEnum)
    {
        if (inventory.ContainsKey(inventoryEnum))
        {
            return inventory[inventoryEnum];
        }
        else
        {
            return 0;
        }
    }
}
