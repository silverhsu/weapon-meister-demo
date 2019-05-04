using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType { DAMAGE_ONLY, KNOCKBACK_LINEAR, KNOCKBACK_RADIAL, PULL, SIDE_HIT}

[System.Serializable]
public class HitInfo {
    public float damageAmount;
    public HitType hitType;
    public Vector2 knockbackDir;
    public float knockbackForce;
    public float knockbackDropoffDist;
    public bool doesOverrideSize;
    public float frameDuration;
}

public class PlayerAttackBasicScript : MonoBehaviour
{
    private List<int> hitObjIDList;
    public HitInfo thisHitInfo;
    public float atkTimer;

    private void Awake()
    {
        thisHitInfo.knockbackDir = this.transform.up;
    }

    void Start()
    {
        hitObjIDList = new List<int>();
    }

    void Update()
    {
        atkTimer += Time.deltaTime;
        if(atkTimer > thisHitInfo.frameDuration * (1 / 60f))
        {
            Destroy(this.gameObject);
        }
    }
}
