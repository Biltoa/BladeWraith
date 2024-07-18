using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PlayerHealth : HealthBase
{
    public PlayerStateMachine Machine;
    public MobStateMachine mobMachine;
    public bool isBlocked;
    public bool isParried;
    public Image healthBar;
    public Image healthBarLower;
    public TextMeshProUGUI healthText;
    protected override void Start()
    {
        base.Start();
    }
    public override void TakeDamage(float amount)
    {
        if(!isBlocked && !isParried)
        {
            base.TakeDamage(amount);
            Machine.SwitchState(Machine.takeDamage);
        }
        else if (isBlocked)
        {
            Machine.blockState.IsAttacked = true;
            base.TakeDamage(amount/3);
        }
        else if (isParried)
        {
            mobMachine.MobHealth.TakeDamage(amount / 2);
        }
        healthBarLower.DOFillAmount(health/100f, 0.5f);
        healthBar.fillAmount = health/100f;
        healthText.text = $"{health}/{100}";
    }

}
