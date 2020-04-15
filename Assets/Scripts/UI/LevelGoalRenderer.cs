using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGoalRenderer : MonoBehaviour
{
    public LevelGoalSystem levelGoalSystem;
    public GameObject goalMain; // TODO these could just be replaced by public textmeshpros
    public GameObject goalOne;
    public GameObject goalTwo;
    public GameObject goalThree;

    public Material orangeMaterial;
    public Material asbestosMaterial;

    private TextMeshProUGUI goalMainText;
    private TextMeshProUGUI goalOneText;
    private TextMeshProUGUI goalTwoText;
    private TextMeshProUGUI goalThreeText;

    private Image goalOneImage;
    private Image goalTwoImage;
    private Image goalThreeImage;
    private Sprite orangeStar;
    private Sprite greyStar;

    private Image mainGoalBoard;
    private Sprite orangeBoard;
    private Sprite greyBoard;

    LevelGoals levelGoals;

    private void Awake()
    {
        goalMainText = goalMain.GetComponentInChildren<TextMeshProUGUI>();
        goalOneText = goalOne.GetComponentInChildren<TextMeshProUGUI>();
        goalTwoText = goalTwo.GetComponentInChildren<TextMeshProUGUI>();
        goalThreeText = goalThree.GetComponentInChildren<TextMeshProUGUI>();

        goalOneImage = transform.Find("LevelGoalCanvas/GoalContainer/GoalOne/Star").GetComponent<Image>();
        goalTwoImage = transform.Find("LevelGoalCanvas/GoalContainer/GoalTwo/Star").GetComponent<Image>();
        goalThreeImage = transform.Find("LevelGoalCanvas/GoalContainer/GoalThree/Star").GetComponent<Image>();
        orangeStar = Resources.Load<Sprite>("UI/LevelGoals/StarIcon");
        greyStar = Resources.Load<Sprite>("UI/LevelGoals/GreyStarIcon");

        mainGoalBoard = transform.Find("LevelGoalCanvas/GoalMain/BackgroundBoard").GetComponent<Image>();
        orangeBoard = Resources.Load<Sprite>("UI/LevelGoals/MainGoalFrame");
        greyBoard = Resources.Load<Sprite>("UI/LevelGoals/MainGoalFrameGrey");
    }

    void Start() // TODO add icons to achievement completion
    { // TODO maybe update text each frame
        levelGoals = levelGoalSystem.GetLevelGoals();

        goalMainText.text = levelGoals.mainGoal.GetGoalDescription();
        goalOneText.text = levelGoals.oneStarGoal.GetGoalDescription();
        goalTwoText.text = levelGoals.twoStarGoal.GetGoalDescription();
        goalThreeText.text = levelGoals.threeStarGoal.GetGoalDescription();
    }

    void Update()
    {
        SetMainGoalColor(goalMainText, mainGoalBoard, levelGoals.mainGoal);
        SetGoalColor(goalOneText, goalOneImage, levelGoals.oneStarGoal);
        SetGoalColor(goalTwoText, goalTwoImage, levelGoals.twoStarGoal);
        SetGoalColor(goalThreeText, goalThreeImage, levelGoals.threeStarGoal);
    }

    void SetGoalColor(TextMeshProUGUI text, Image image, ILevelGoal levelGoal)
    {
        if (levelGoal.IsRequirementMet())
        {
            text.fontSharedMaterial = orangeMaterial;
            image.sprite = orangeStar;
        }
        else
        {
            text.fontSharedMaterial = asbestosMaterial;
            image.sprite = greyStar;
        }
    }

    void SetMainGoalColor(TextMeshProUGUI text, Image image, ILevelGoal levelGoal)
    {
        if (levelGoal.IsRequirementMet())
        {
            text.fontSharedMaterial = orangeMaterial;
            image.sprite = orangeBoard;
            image.color = new Color32(255, 255, 255, 205);
        }
        else
        {
            text.fontSharedMaterial = asbestosMaterial;
            image.sprite = greyBoard;
            image.color = new Color32(255, 255, 255, 127);
        }
    }
}
