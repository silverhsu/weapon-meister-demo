using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    public List<int> hitObjIDList;
    private Rigidbody2D thisRigid2D;


    void Start()
    {
        hitObjIDList = new List<int>();
        thisRigid2D = this.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collidedCol)
    {
        if (!hitObjIDList.Contains(collidedCol.gameObject.GetHashCode()))
        {
            hitObjIDList.Add(collidedCol.gameObject.GetHashCode());
            HitInfo tempHitInfo = collidedCol.gameObject.GetComponent<PlayerAttackBasicScript>().thisHitInfo;
            Vector3 tempDeltaVec = this.transform.position - collidedCol.gameObject.transform.position;
            switch (tempHitInfo.hitType)
            {
                case HitType.KNOCKBACK_LINEAR:
                    thisRigid2D.AddForce(tempHitInfo.knockbackDir * tempHitInfo.knockbackForce, ForceMode2D.Impulse);
                    break;

                case HitType.KNOCKBACK_RADIAL:
                    thisRigid2D.AddForce(tempDeltaVec.normalized * tempHitInfo.knockbackForce, ForceMode2D.Impulse);
                    break;

                case HitType.SIDE_HIT:
                    tempDeltaVec = this.transform.position - collidedCol.gameObject.transform.position;
                    float tempDot = Vector3.Dot(tempDeltaVec.normalized, collidedCol.gameObject.transform.right);
                    Vector3 tempForceDir = collidedCol.gameObject.transform.right * tempDot;
                    tempForceDir += collidedCol.gameObject.transform.up * 0.7f;
                    tempForceDir.Normalize();
                    thisRigid2D.AddForce(tempForceDir * tempHitInfo.knockbackForce, ForceMode2D.Impulse);
                    break;

                default:
                    Debug.Log("UNKNOWN HIT TYPE");
                    thisRigid2D.AddForce(tempHitInfo.knockbackDir * tempHitInfo.knockbackForce, ForceMode2D.Impulse);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hitObjIDList.Contains(collision.gameObject.GetHashCode()))
        {
            hitObjIDList.Remove(collision.gameObject.GetHashCode());
        }
    }
}
