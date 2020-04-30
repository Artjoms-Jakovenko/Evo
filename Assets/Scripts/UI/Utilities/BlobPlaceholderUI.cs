using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobPlaceholderUI
{
    private string assetPath;
    private GameObject _asset;
    private string iconPath;
    private Sprite _icon;
    public BlobPlaceholderUI(string assetPath, string iconPath)
    {
        this.assetPath = assetPath;
        this.iconPath = iconPath;
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

    public Sprite Icon
    {
        get
        {
            if (_icon == null)
            {
                _icon = Resources.Load<Sprite>(iconPath);
            }
            return _icon;
        }
        set
        {
            _icon = value;
        }
    }
}
