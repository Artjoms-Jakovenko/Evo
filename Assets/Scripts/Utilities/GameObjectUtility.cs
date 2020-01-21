using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtility
{
    public static GameObject InstantiateChild(GameObject asset, GameObject parent, bool resetScale = false)
    {
        GameObject gameObject = GameObject.Instantiate(asset);
        MakeChild(gameObject, parent);
        if (resetScale)
        {
            gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
        }

        return gameObject;
    }

    public static void MakeChild(GameObject child, GameObject parent)
    {
        child.transform.SetParent(parent.transform);
    }
}
