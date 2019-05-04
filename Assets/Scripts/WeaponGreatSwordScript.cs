using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGreatSwordScript : WeaponControlScript
{
    [HideInInspector]
    public PlayerControlScript playerScript;
    public GameObject attackPrefab;

    public float swordAttackOffsetDistance = 1f;

    void Awake()
    {
        playerScript = GameObject.FindObjectOfType<PlayerControlScript>();
    }

    public override void Start()
    {
        Debug.Log("Player Great Sword made");
        this.transform.position = playerScript.neutralHandTrans.position;
        this.transform.parent = playerScript.handsAnchor.transform;
    }

    public override void Update()
    {
        updateTimers();
        if (isAtkOnCooldown)
        {
            Util.MatchToTransform(this.transform, playerScript.finishHandTrans);
        }
    }

    public override void OnWeaponButtonPressed()
    {

    }

    public override void OnWeaponButtonHeld()
    {
        if (!isAtkOnCooldown)
        {
            Util.MatchToTransform(this.transform, playerScript.heldHandTrans);
            if (isMovementStopOnAttack)
            {
                playerScript.thisRigid2D.velocity = Vector2.zero;
            }
        }
    }

    public override void OnWeaponCooldownComplete()
    {
        Util.MatchToTransform(this.transform, playerScript.neutralHandTrans);
    }


    public override void OnWeaponDelayComplete()
    {
        Instantiate(attackPrefab, playerScript.transform.position + (swordAttackOffsetDistance * new Vector3(playerScript.inputRightStick.x, playerScript.inputRightStick.y, 0f)), Util.UnitVectorToQuaternion(playerScript.inputRightStick));
        atkCooldownTimer = atkCooldownThreshold;
    }


    public override void OnWeaponButtonReleased()
    {
        if (!isAtkOnCooldown && !isWeaponDelayed)
        {
            atkCooldownTimer = weaponDelayThreshold;

            isWeaponDelayed = true;
            weaponDelayTimer = weaponDelayThreshold;
        }
        this.transform.position = playerScript.finishHandTrans.position;
    }
}