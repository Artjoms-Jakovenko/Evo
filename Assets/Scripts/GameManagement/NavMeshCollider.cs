using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NavMeshTriangulation navmeshData = NavMesh.CalculateTriangulation();
        Mesh mesh = new Mesh();
        mesh.SetVertices(navmeshData.vertices.ToList());
        mesh.SetIndices(navmeshData.indices, MeshTopology.Triangles, 0);

        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
