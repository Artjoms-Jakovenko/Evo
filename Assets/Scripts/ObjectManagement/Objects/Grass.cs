using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Edible
{
    public override void HandleDestruction()
    {
        GetComponentInChildren<Animator>().enabled = true;
        ObjectManager.GetInstance().DestroyObject(gameObject, 0.625F);
    }
}
