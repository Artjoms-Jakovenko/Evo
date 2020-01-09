using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtility
{
    public static GameObject InstantiateChild(GameObject asset, GameObject parent)
    {
        GameObject gameObject = GameObject.Instantiate(asset);
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);

        return gameObject;
    }
}
