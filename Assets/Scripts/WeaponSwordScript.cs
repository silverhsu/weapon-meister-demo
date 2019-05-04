using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwordScript : WeaponControlScript
{
    [HideInInspector]
    public PlayerControlScript playerScript;
    public GameObject attackPrefab;

    void Awake()
    {
        playerScript = GameObject.FindObjectOfType<PlayerControlScript>();
    }

    public override void Start()
    {
        Debug.Log("Player Sword made");
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
        }
    }

    public override void OnWeaponCooldownComplete()
    {
        Util.MatchToTransform(this.transform, playerScript.neutralHandTrans);
    }

    public override void OnWeaponButtonReleased()
    {
        if (!isAtkOnCooldown)
        {
            atkCooldownTimer = atkCooldownThreshold;
            Instantiate(attackPrefab, playerScript.transform.position + new Vector3(playerScript.inputRightStick.x, playerScript.inputRightStick.y, 0f), Util.UnitVectorToQuaternion(playerScript.inputRightStick));
            if (isMovementStopOnAttack)
            {
                playerScript.thisRigid2D.velocity = Vector2.zero;
            }
        }
        this.transform.position = playerScript.finishHandTrans.position;
    }
}
