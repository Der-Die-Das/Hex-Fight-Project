using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{

    Mesh hexMesh;
    List<Vector3> vertices;
    List<Color> colors;
    List<int> triangles;

    MeshCollider meshCollider;

    public void Triangulate(HexCell[] cells)
    {
        checkVariableDeclaration();

        hexMesh.Clear();
        vertices.Clear();
        colors.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
        meshCollider.sharedMesh = hexMesh;
    }

    void checkVariableDeclaration()
    {
        if (hexMesh == null)
        {
            GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
            hexMesh.name = "Hex Mesh";
        }
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        if (vertices == null)
        {
            vertices = new List<Vector3>();
        }
        if (colors == null)
        {
            colors = new List<Color>();
        }
        if (triangles == null)
        {
            triangles = new List<int>();
        }
    }

    void Triangulate(HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.corners[i],
                center + HexMetrics.corners[i + 1]
            );
            //AddTriangleColor(cell.color);
        }
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }
}