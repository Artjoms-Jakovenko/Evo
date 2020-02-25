using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGoalRenderer : MonoBehaviour
{
    public LevelGoalSystem levelGoalSystem;
    public GameObject goalOne;
    public GameObject goalTwo;
    public GameObject goalThree;

    private TextMeshProUGUI goalOneText;
    private TextMeshProUGUI goalTwoText;
    private TextMeshProUGUI goalThreeText;

    List<ILevelGoal> levelGoals;

    private void Awake()
    {
        goalOneText = goalOne.GetComponentInChildren<TextMeshProUGUI>();
        goalTwoText = goalTwo.GetComponentInChildren<TextMeshProUGUI>();
        goalThreeText = goalThree.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start() // TODO add icons to achievement completion
    {
        levelGoals = levelGoalSystem.GetLevelGoals();

        goalOneText.text = levelGoals[0].GetGoalDescription();
        goalTwoText.text = levelGoals[1].GetGoalDescription();
        goalThreeText.text = levelGoals[2].GetGoalDescription();
    }

    void Update()
    {
        SetGoalColor(goalOneText, levelGoals[0]);
        SetGoalColor(goalTwoText, levelGoals[1]);
        SetGoalColor(goalThreeText, levelGoals[2]);
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
