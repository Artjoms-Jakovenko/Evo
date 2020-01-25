using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject evolveShopScreen;
    private UpgradeShop upgradeShop;

    private void Start()
    {
        upgradeShop = evolveShopScreen.GetComponent<UpgradeShop>();
    }

    public void StartEvolveShopScreen()
    {
        DisableUI();
        upgradeShop.EnableUI();
    }

    public void EnableUI()
    {
        mainMenuCanvas.SetActive(true);
    }
    public void DisableUI()
    {
        mainMenuCanvas.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TestingGround");
    }
}
