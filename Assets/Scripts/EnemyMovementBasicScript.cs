using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBasicScript : MonoBehaviour
{
    private Rigidbody2D thisRigid2D;
    private Collider2D thisCol2D;
    public GameObject playerObj;

    private void Awake()
    {
        thisRigid2D = this.GetComponent<Rigidbody2D>();
        thisCol2D = this.GetComponent<Collider2D>();
        playerObj = GameObject.FindObjectOfType<PlayerControlScript>().gameObject;
    }

    void FixedUpdate()
    {
        Vector3 deltaVec = playerObj.transform.position - this.transform.position;
        thisRigid2D.AddForce(deltaVec.normalized * 0.2f * thisRigid2D.mass, ForceMode2D.Impulse);
    }
}
