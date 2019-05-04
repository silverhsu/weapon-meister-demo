using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFPSScript : MonoBehaviour
{
    private TextMesh txtMesh;
    void Start()
    {
        txtMesh = this.GetComponent<TextMesh>();
    }

    void Update()
    {
        txtMesh.text = "F:" + ((int)((1f / Time.smoothDeltaTime) / 10) * 10);
    }
}
