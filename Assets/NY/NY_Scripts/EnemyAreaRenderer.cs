using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class EnemyAreaRenderer : MonoBehaviour
{
    public EnemyPropaty enemyProperty;

    private MeshFilter _meshFilter;

    private MeshFilter MeshFilter
    {
        get
        {
            if (_meshFilter == null)
                _meshFilter = GetComponent<MeshFilter>();
            return _meshFilter;
        }
    }

    private void Awake()
    {
        MeshFilter.mesh = CreateFanMesh(enemyProperty.FovAngle, 16);
        transform.localScale = Vector3.one * enemyProperty.FovLength;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    #region Fan

    private static Vector3[] CreateFanVertices(float iAngle, int iTriangleCount)
    {
        if (iAngle <= 0.0f)
        {
            throw new System.ArgumentException($"角度がおかしい！ iAngle={iAngle}");
        }

        if (iTriangleCount <= 0)
        {
            throw new System.ArgumentException($"数がおかしい！ iTriangleCount={iTriangleCount}");
        }

        iAngle = Mathf.Min(iAngle, 360.0f);

        var vertices = new List<Vector3>(iTriangleCount + 2);

        // 始点
        vertices.Add(Vector3.zero);

        // Mathf.Sin()とMathf.Cos()で使用するのは角度ではなくラジアンなので変換しておく。
        float radian = iAngle * Mathf.Deg2Rad;
        float startRad = -radian / 2;
        float incRad = radian / iTriangleCount;

        for (int i = 0; i < iTriangleCount + 1; ++i)
        {
            float currentRad = startRad + (incRad * i);

            Vector3 vertex = new Vector3(Mathf.Sin(currentRad), 0.0f, Mathf.Cos(currentRad));
            vertices.Add(vertex);
        }

        return vertices.ToArray();
    }

    private static Mesh CreateFanMesh(float iAngle, int iTriangleCount)
    {
        var mesh = new Mesh();

        var vertices = CreateFanVertices(iAngle, iTriangleCount);

        var triangleIndexes = new List<int>(iTriangleCount * 3);

        for (int i = 0; i < iTriangleCount; ++i)
        {
            triangleIndexes.Add(0);
            triangleIndexes.Add(i + 1);
            triangleIndexes.Add(i + 2);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangleIndexes.ToArray();

        mesh.RecalculateNormals();

        return mesh;
    }

    #endregion
}
