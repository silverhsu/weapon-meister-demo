using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public List<GameObject> targetObjList;
    public float cameraFollowFactor = 9f;
    private Vector3 accumulatedPos;
    void Start()
    {
        
    }

    void Update()
    {
        accumulatedPos = Vector3.zero;
        foreach(GameObject tempObj in targetObjList)
        {
            accumulatedPos += tempObj.transform.position;
        }
        accumulatedPos /= targetObjList.Count;
        this.transform.position = Vector3.Lerp(this.transform.position, accumulatedPos, cameraFollowFactor * Time.deltaTime);
    }
}
