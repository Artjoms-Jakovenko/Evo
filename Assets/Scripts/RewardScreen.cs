using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardScreen : MonoBehaviour
{
    public GameObject nextLevelButton;

    string nextLevelName;
    private void Awake()
    {
        nextLevelName = LevelManager.GetNextLevelName(SceneManager.GetActiveScene().name);
        if(nextLevelName == null)
        {
            nextLevelButton.SetActive(false);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
