using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGoalRenderer : MonoBehaviour
{
    public LevelGoalSystem levelGoalSystem;
    public GameObject goalMain; // TODO these could just be replaced by public textmeshpros
    public GameObject goalOne;
    public GameObject goalTwo;
    public GameObject goalThree;

    private TextMeshProUGUI goalMainText;
    private TextMeshProUGUI goalOneText;
    private TextMeshProUGUI goalTwoText;
    private TextMeshProUGUI goalThreeText;

    LevelGoals levelGoals;

    private void Awake()
    {
        goalMainText = goalMain.GetComponentInChildren<TextMeshProUGUI>();
        goalOneText = goalOne.GetComponentInChildren<TextMeshProUGUI>();
        goalTwoText = goalTwo.GetComponentInChildren<TextMeshProUGUI>();
        goalThreeText = goalThree.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start() // TODO add icons to achievement completion
    {
        levelGoals = levelGoalSystem.GetLevelGoals();

        goalMainText.text = levelGoals.mainGoal.GetGoalDescription();
        goalOneText.text = levelGoals.oneStarGoal.GetGoalDescription();
        goalTwoText.text = levelGoals.twoStarGoal.GetGoalDescription();
        goalThreeText.text = levelGoals.threeStarGoal.GetGoalDescription();
    }

    void Update()
    {
        SetGoalColor(goalMainText, levelGoals.mainGoal);
        SetGoalColor(goalOneText, levelGoals.oneStarGoal);
        SetGoalColor(goalTwoText, levelGoals.twoStarGoal);
        SetGoalColor(goalThreeText, levelGoals.threeStarGoal);
    }

    void SetGoalColor(TextMeshProUGUI text, ILevelGoal levelGoal)
    {
        if (levelGoal.IsRequirementMet())
        {
            text.color = Color.green;
        }
        else
        {
            text.color = Color.grey;
        }
    }
}
