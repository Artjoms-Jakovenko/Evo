using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    public int money = 0;

    public void AddMoney(int amount)
    {
        money += amount;
    }
}
