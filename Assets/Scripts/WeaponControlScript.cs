using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponEnum { SWORD, RAPIER, WHIP, GREAT_SWORD, HAMMER}

// Framework weapon controller, extended per weapon basis
public class WeaponControlScript : MonoBehaviour
{
    public HitInfo swingHitInfo;

    public bool isAtkOnCooldown;
    public float atkCooldownTimer;
    public float atkCooldownThreshold;
    public bool isWeaponDelayed;
    public float weaponDelayTimer;
    public float weaponDelayThreshold;

    public bool isMovementStopOnAttack;

    public virtual void Start()
    {
        Debug.Log("virtual start");
    }

    public virtual void Update()
    {
        Debug.Log("virtual update");
    }

    public virtual void OnWeaponButtonPressed()
    {
        Debug.Log("Weapon Button Pressed");
    }

    public virtual void OnWeaponButtonHeld()
    {
        Debug.Log("Weapon Button Held");
    }

    public virtual void OnWeaponButtonReleased()
    {
        Debug.Log("Weapon Button Released");
    }

    public virtual void OnWeaponCooldownComplete()
    {

    }

    public virtual void OnWeaponDelayComplete()
    {

    }

    public void updateTimers()
    {
        if (isAtkOnCooldown){
            atkCooldownTimer -= Time.deltaTime;
        }
        if (isWeaponDelayed){
            weaponDelayTimer -= Time.deltaTime;
        }

        // ATTACK COOLDOWNS
        if (atkCooldownTimer > 0)
        {
            atkCooldownTimer -= Time.deltaTime;
            isAtkOnCooldown = true;
        }
        else
        {
            atkCooldownTimer = 0f;
            if (isAtkOnCooldown)
            {
                isAtkOnCooldown = false;
                OnWeaponCooldownComplete();
            }
        }

        // WEAPON DELAYS
        if (weaponDelayTimer > 0)
        {
            weaponDelayTimer -= Time.deltaTime;
            isWeaponDelayed = true;
        }
        else
        {
            weaponDelayTimer = 0f;
            if (isWeaponDelayed)
            {
                isWeaponDelayed = false;
                OnWeaponDelayComplete();
            }
        }
    }
}
