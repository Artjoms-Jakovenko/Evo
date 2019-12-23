using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject evolveShopScreen;
    private UpgradeShop upgradeShop;

    private void Awake()
    {
        upgradeShop = evolveShopScreen.GetComponent<UpgradeShop>();
    }

    public void StartEvolveShopScreen()
    {
        mainMenuCanvas.SetActive(false);
        upgradeShop.EnableUI();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TestingGround");
    }
}
