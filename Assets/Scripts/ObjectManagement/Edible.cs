using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour
{
    public float portionEnergyValue = 0.0F;
    public int portionNumber = 1;

    public void Awake()
    {
        gameObject.GetComponent<TaggedObject>().AddTag(ObjectTag.Edible);
    }

    public float Bite(int biteSize)
    {
        if(biteSize >= portionNumber)
        {
            HandleDestruction();
            return portionNumber * portionEnergyValue;
        }
        else
        {
            portionNumber -= biteSize;
            return portionNumber * biteSize;
        }
    }

    public virtual void HandleDestruction()
    {
        ObjectManager.GetInstance().DestroyObject(gameObject);
    }
}
