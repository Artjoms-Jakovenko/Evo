using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoneyOnCreation : MonoBehaviour
{
    public int value = 0;
    private void Start()
    {
        GameObject.Find("PointCounter").GetComponent<PointCounter>().AddMoney(value);
    }
}
