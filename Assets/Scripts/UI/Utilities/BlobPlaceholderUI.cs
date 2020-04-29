using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobPlaceholderUI
{
    private string assetPath;
    private GameObject _asset;
    public BlobPlaceholderUI(string assetPath)
    {
        this.assetPath = assetPath;
    }
    public GameObject Asset
    {
        get
        {
            if (_asset == null)
            {
                _asset = Resources.Load<GameObject>(assetPath);
            }
            return _asset;
        }
        set
        {
            _asset = value;
        }
    }
}
