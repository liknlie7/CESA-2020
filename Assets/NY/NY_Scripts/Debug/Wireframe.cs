using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wireframe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0), MeshTopology.Lines, 0);
    }
}
