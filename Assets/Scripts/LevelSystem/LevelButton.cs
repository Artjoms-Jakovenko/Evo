using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public LevelEnum associatedLevel;

    private void Awake()
    {
        if (!LevelManager.IsLevelUnlocked(associatedLevel))
        {
            Debug.Log("Not unlocked " + associatedLevel.ToString()); // TODO change color or so
            gameObject.SetActive(false); // TODO rework into make nonclickable
        }
    }

    private void OnMouseDown()
    {
        if (LevelManager.IsLevelUnlocked(associatedLevel))
        {
            LevelManager.StartLevel(associatedLevel);
        }
    }
}
