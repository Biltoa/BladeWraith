using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int PowerupPotions;
    public int HealPotions;
    public TextMeshProUGUI PowerupText;
    public TextMeshProUGUI HealText;
    void OnEnable()
    {
        PowerupPotions = PlayerPrefs.GetInt("powerupPotions");
        HealPotions = PlayerPrefs.GetInt("healPotions");
        ChangeUIText();
    }

    public void PickupPowerupPotion()
    {
        PowerupPotions++;
        PlayerPrefs.SetInt("powerupPotions", PowerupPotions);
        ChangeUIText();
    }

    public void PickupHealPotion()
    {
        HealPotions++;
        PlayerPrefs.SetInt("healPotions", PowerupPotions);
        ChangeUIText();
    }

    public void DropPowerupPotion()
    {
        PowerupPotions--;
        PlayerPrefs.SetInt("powerupPotions", PowerupPotions);
        ChangeUIText();
    }

    public void DropHealPotion()
    {
        HealPotions--;
        PlayerPrefs.SetInt("healPotions", PowerupPotions);
        ChangeUIText();
    }

    private void ChangeUIText()
    {
        PowerupText.text = PowerupPotions.ToString();
        HealText.text = HealPotions.ToString();
    }
}
