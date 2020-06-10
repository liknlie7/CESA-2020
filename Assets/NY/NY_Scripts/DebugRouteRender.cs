using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイ時にMeshRenderを消す
public class DebugRouteRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}
