using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public LevelEnum associatedLevel;
    
    private GameObject leftStar;
    private GameObject centerStar;
    private GameObject rightStar;
    private Light glowLight;
    private GameObject crystal;

    private void Init()
    {
        leftStar = transform.Find("LeftPegStar").gameObject;
        centerStar = transform.Find("CenterPegStar").gameObject;
        rightStar = transform.Find("RightPegStar").gameObject;
        glowLight = transform.Find("Glow").GetComponent<Light>();
        crystal = transform.Find("LevelPeg/Crystal").gameObject;
    }

    private void Awake()
    {
        if (!LevelManager.IsLevelUnlocked(associatedLevel))
        {
            Debug.Log("Not unlocked " + associatedLevel.ToString()); // TODO change color or so
            gameObject.SetActive(false); // TODO rework into make nonclickable
        }
        else
        {
            Init(); // Only init if object is active

            switch (LevelManager.GetLevelStarCount(associatedLevel))
            {
                case 0:
                    if (LevelManager.IsLevelCompleted(associatedLevel))
                    {
                        glowLight.intensity = 600.0F;
                        crystal.GetComponent<Renderer>().material = Resources.Load<Material>("UI/LevelSelectScreen/GreenGlow");  
                    }
                    break;
                case 1:
                    glowLight.intensity = 700.0F;
                    crystal.GetComponent<Renderer>().material = Resources.Load<Material>("UI/LevelSelectScreen/BlueGlow");
                    centerStar.SetActive(true);
                    break;
                case 2:
                    glowLight.intensity = 800.0F;
                    crystal.GetComponent<Renderer>().material = Resources.Load<Material>("UI/LevelSelectScreen/PurpleGlow");
                    leftStar.SetActive(true);
                    rightStar.SetActive(true);
                    break;
                case 3:
                    glowLight.intensity = 1000.0F;

                    crystal.GetComponent<Renderer>().material = Resources.Load<Material>("UI/LevelSelectScreen/OrangeGlow");
                    leftStar.SetActive(true);
                    centerStar.SetActive(true);
                    rightStar.SetActive(true);
                    break;
            }
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
