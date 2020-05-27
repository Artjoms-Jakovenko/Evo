using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour
{
    public ObjectTag tage;
    public int count;

    public GameObject locked;
    public GameObject actual;

    bool unlocked = false;



    // Update is called once per frame
    void Update()
    {
        if (unlocked)
        {
            return;
        }
        if (ObjectManager.GetInstance().GetAllWithTags(new List<ObjectTag>(){ tage }).Count >= count)
        {
            UnlockObject();
        }
    }

    void UnlockObject()
    {
        unlocked = true;
        locked.SetActive(false);
        actual.SetActive(true);
    }
}
