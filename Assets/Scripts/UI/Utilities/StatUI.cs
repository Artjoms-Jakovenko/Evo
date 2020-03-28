using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUI
{
    public string statDisplayName;
    public string statDescription;
    public StatUI(string iconPath)
    {
        this.iconPath = iconPath;
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
    private string iconPath;
    private Sprite _icon;
}
