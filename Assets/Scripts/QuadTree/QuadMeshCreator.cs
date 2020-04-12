using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class QuadMeshCreator : MonoBehaviour
{
    public bool generate = false;
    public DestructibleTerrain terrain;
    public Material voxelMaterial;

    private GameObject previusMesh;
    private bool initialized;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(terrain.quadTree != null)
        {
            initialized = true;
            terrain.quadTree.QuadTreeUpdated += (obj, args) => { generate = true; };

        }
        if (generate)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var generatedMesh = GenerateMesh();
            stopwatch.Stop();

            if (previusMesh != null)
                Destroy(previusMesh);
            previusMesh = generatedMesh;
            generate = false;
            UnityEngine.Debug.Log("Tardo: " + stopwatch.ElapsedMilliseconds);
        }
    }


    private GameObject GenerateMesh()
    {
        //Debug.Log("Generando Terreno");
        GameObject chunk = new GameObject();
        chunk.name = "VoxelChunk";
        chunk.transform.parent = transform;
        chunk.transform.localPosition = Vector3.zero;

        var mesh = new Mesh();
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();
        var normals = new List<Vector3>();

        // Agarro todas las hojas que tengan valor verdaderos, es decir que esten activas
        foreach (var leaf in terrain.quadTree.GetLeafNodes().Where(t=>t.value))
        {
            var upperLeft = new Vector3(leaf.Position.x - leaf.Size * 0.5f, leaf.Position.y + leaf.Size * 0.5f, 0);
            var initialIndex = vertices.Count;
           
            // Creo los vertices de mi cuadrado
            vertices.Add(upperLeft);
            vertices.Add(upperLeft + Vector3.right * leaf.Size);
            vertices.Add(upperLeft + Vector3.down * leaf.Size);;
            vertices.Add(upperLeft + Vector3.down * leaf.Size + Vector3.right * leaf.Size);
           
            // los UVS son iguales que tus vertices para que funcione bien el tiling
            uvs.Add(upperLeft);
            uvs.Add(upperLeft + Vector3.right * leaf.Size);
            uvs.Add(upperLeft + Vector3.down * leaf.Size);;
            uvs.Add(upperLeft + Vector3.down * leaf.Size + Vector3.right * leaf.Size);
            
            // las nromales apuntan todas al mismo lado por que es 2D, en la direccion opuesta a la luz
            normals.Add(Vector3.back);
            normals.Add(Vector3.back);
            normals.Add(Vector3.back);
            normals.Add(Vector3.back);
           
            // Creo el primer triangulo que forma al cuadrado
            triangles.Add(initialIndex);
            triangles.Add(initialIndex + 1);
            triangles.Add(initialIndex + 2);
            
            // Creo el segundo triangulo que forma al cuadrado
            triangles.Add(initialIndex + 3);
            triangles.Add(initialIndex + 2);
            triangles.Add(initialIndex + 1);

        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        mesh.SetNormals(normals);

        var meshFilter = chunk.AddComponent<MeshFilter>();
        var meshRenderer = chunk.AddComponent<MeshRenderer>();
        meshRenderer.material = voxelMaterial;
        meshRenderer.sortingLayerName = "PlanetLayer";
        meshRenderer.sortingOrder = 1;
        meshFilter.mesh = mesh;

        return chunk;
    }
}
