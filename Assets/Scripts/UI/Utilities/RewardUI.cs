using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI
{
    public RewardUI(string iconPath)
    {
        this.iconPath = iconPath;
    }
    public Sprite Icon
    {
        get
        {
            if (_icon == null)
            {
                _icon = Resources.Load<Sprite>(iconPath); // TODO
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
