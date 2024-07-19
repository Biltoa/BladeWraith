using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachineBase
{
    public float speed = 10f;
    public float acceleration = 2f;
    public float deAcceleration = 1f;
    public float rotationSpeed = 1f;
    public float currentAcceleration;

    public bool isHeavyAttack;
    public bool isLightAttack;
    public bool isRangedAttack;
    public bool isPowerUped;

    public Vector3 lastDirection;

    public CharacterController Controller;
    public Animator Animator;
    public Inventory PlayerInventory;
    public PlayerHealth PlayerHealth;
    public GameObject sword;
    public UIManager UIManager;
    public SpawnManager SpawnManager;

    [Header("States")]
    public PlayerState_Idle idleState;
    public PlayerState_Walk walkState;
    public PlayerState_LightAttack1 lightAttack1;
    public PlayerState_LightAttack2 lightAttack2;
    public PlayerState_LightAttack3 lightAttack3;
    public PlayerState_HeavyAttack1 heavyAttack1;
    public PlayerState_HeavyAttack2 heavyAttack2;
    public PlayerState_HeavyAttack3 heavyAttack3;
    public PlayerState_RangedAttack1 rangedAttack1;
    public PlayerState_RangedAttack2 rangedAttack2;
    public PlayerState_Roll rollState;
    public PlayerState_Block blockState;
    public PlayerState_Parry parryState;
    public PlayerState_TakeDamage takeDamage;
    public PlayerState_Pickup pickupState;
    public PlayerState_DrinkPotion drinkPotion;

    [Header("Powerup timer")]
    public float powerTimer;

    [Header("Particles")]
    public ParticleSystem HealingParticle;
    public ParticleSystem PowerupParticle;

    [Header("Audio")]
    public AudioSource PlayerDeath;
    public AudioSource PlayerDamage;
    public AudioSource MonsterDamage;
    public AudioSource MonsterDeath;
    public AudioSource SpellCast;
    public AudioSource SpellImpact;
    public AudioSource Footsteps;
    public AudioSource Buff;
    public AudioSource EnemyAttack;
    public AudioSource SwordSlash;

    [Header("UI Button Input")]
    public bool LightAttack;
    public bool HeavyAttack;
    public bool SwitchAttack;
    public bool Roll;
    public bool Block;
    public bool Parry;
    public bool Pickup;
    public bool DrinkPower;
    public bool DrinkHealth;

    protected override void SetInitialState()
    {
        currentState = idleState;
    }

    public void SetCoins()
    {
        UIManager.SetCoins();
    }

    public void PowerUp()
    {
        isPowerUped = true;
    }

    public void EnableAttackInput(int input)
    {
        switch (input)
        {
            case 0:
                LightAttack = true;
                break;
            case 1:
                HeavyAttack = true;
                break;
            case 2:
                SwitchAttack = !SwitchAttack;
                break;
            case 3:
                LightAttack = false;
                break;
            case 4:
                HeavyAttack = false;
                break;
            default:
                break;
        }
    }

    public void EnableSkillsInput(int input)
    {
        switch (input)
        {
            case 0:
                Roll = true;
                break;
            case 1:
                Block = !Block;
                break;
            case 2:
                Parry = true;
                break;
            case 3:
                Pickup = true;
                break;
            case 4:
                DrinkPower = true;
                break;
            case 5:
                DrinkHealth = true;
                break;
            default:
                break;
        }
    }



    protected override void Update()
    {
        base.Update();
        powerTimer += Time.deltaTime;
        if (powerTimer >= 25f)
        {
            isPowerUped = false;
            PowerupParticle.Stop();
        }
    }
}

