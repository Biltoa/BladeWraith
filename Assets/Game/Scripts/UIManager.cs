using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject meleeSkills;
    public GameObject rangeSkills;


    public GameObject PlayerHealth;
    public GameObject Skills;
    public GameObject PauseButton;
    public GameObject Potions;

    public Inventory Inventory;

    public TextMeshProUGUI CoinsValue;
    public TextMeshProUGUI LevelText;

    public GameObject GameWon;
    public GameObject GameLost;

    public GameObject SpawnManager;
    //public GameObject mainPlayer;
    public GameObject PotionObjects;
    public GameObject GameStart;
    public GameObject Joystick;
    public GACustomEvents GameAnalytics;
    private int coins;

    private void Start()
    {
        SetCoins();
        SetLevelUI();
        if (PlayerPrefs.GetInt("ProceedLevel") == 1)
        {
            StartGame();
            PlayerPrefs.SetInt("ProceedLevel", 0);
        }
    }

    public void SetLevelUI()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        LevelText.text = "Level " + PlayerPrefs.GetInt("Level").ToString();
    }

    public void SetCoins()
    {
        CoinsValue.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void ChangeSkillImages()
    {
        meleeSkills.SetActive(!meleeSkills.activeSelf);
        rangeSkills.SetActive(!rangeSkills.activeSelf);
    }

    public void BuyPotion(int type)
    {
        coins = PlayerPrefs.GetInt("Coins");
        if (type == 0 && coins >= 100)
        {
            PlayerPrefs.SetInt("Coins", coins - 100);
            SetCoins();
            Inventory.PickupHealPotion();
        }
        else if (type == 1 && coins >= 150)
        {
            PlayerPrefs.SetInt("Coins", coins - 150);
            SetCoins();
            Inventory.PickupPowerupPotion();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        PlayerHealth.SetActive(true);
        Skills.SetActive(true);
        PauseButton.SetActive(true);
        Potions.SetActive(true);
        //mainPlayer.SetActive(true);
        SpawnManager.SetActive(true);
        PotionObjects.SetActive(true);
        GameStart.SetActive(false);
        Joystick.SetActive(true);
    }

    public void PauseGame()
    {
        PlayerHealth.SetActive(false);
        Skills.SetActive(false);
        PauseButton.SetActive(false);
        Potions.SetActive(false);
        Joystick.SetActive(false);
    }

    public void GameWin()
    {
        GameWon.SetActive(true);
        int level = PlayerPrefs.GetInt("Level") + 1;
        PlayerPrefs.SetInt("Level", level);
        GameAnalytics.levelSuccess("Level "+ level, level);
    }

    public void GameLose()
    {
        GameLost.SetActive(true);
    }

    public void ReloadScene(int behavior)
    {
        PlayerPrefs.SetInt("ProceedLevel", behavior);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
