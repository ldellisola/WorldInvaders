using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class QuadMeshCreator : MonoBehaviour
{
    public bool generate = false;
    public ArrayDestructibleTerrain ArrayTerrain;
    public DestructibleTerrain Terrain;
    public Material voxelMaterial;

    private GameObject previusMesh;
    private bool initialized = false;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ArrayTerrain!=null && ArrayTerrain.isActiveAndEnabled)
        {
            if (ArrayTerrain.quadTree != null && !initialized)
            {
                initialized = true;
                ArrayTerrain.quadTree.QuadTreeUpdated += (obj, args) => { generate = true; };

            }
            if (generate)
            {

                var generatedMesh = GenerateMesh(ArrayTerrain);

                if (previusMesh != null)
                    Destroy(previusMesh);
                previusMesh = generatedMesh;
                generate = false;
            }
        }
        else if (Terrain!= null && Terrain.isActiveAndEnabled)
            
        {

            if (Terrain.quadTree != null && !initialized)
            {
                initialized = true;
                Terrain.quadTree.QuadTreeUpdated += (obj, args) => { generate = true; };

            }
            if (generate)
            {
                var generatedMesh = GenerateMesh(Terrain);

                if (previusMesh != null)
                    Destroy(previusMesh);
                previusMesh = generatedMesh;
                generate = false;
            }
        }
    }


    private GameObject GenerateMesh(ArrayDestructibleTerrain terrain)
    {
        //Debug.Log("Generando Terreno");
        GameObject chunk = new GameObject();
        chunk.name = "VoxelChunk";
        chunk.transform.parent = transform;
        chunk.transform.localPosition = new Vector3(0,0,-1);
        chunk.transform.position = new Vector3(0, 0, -1);

        var collider = chunk.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.transform.parent = chunk.transform;
        collider.size = new Vector2(terrain.size, terrain.size);
        collider.offset = transform.position;

        var trigger = chunk.AddComponent<DestructibleTerrainCollision>();
        trigger.ArrayTerrain = terrain;



        var mesh = new Mesh();
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();
        var normals = new List<Vector3>();

        // Agarro todas las hojas que tengan valor verdaderos, es decir que esten activas
        foreach (var leaf in terrain.quadTree.GetLeafNodes().Where(t=>t.Data))
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
    private GameObject GenerateMesh(DestructibleTerrain terrain)
    {
        //Debug.Log("Generando Terreno");
        GameObject chunk = new GameObject();
        chunk.name = "VoxelChunk";
        chunk.transform.parent = transform;
        chunk.transform.localPosition = new Vector3(0,0,-1);
        chunk.transform.position = new Vector3(0, 0, -1);

        var collider = chunk.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.transform.parent = chunk.transform;
        collider.size = new Vector2(terrain.size, terrain.size);
        collider.offset = transform.position;

        var trigger = chunk.AddComponent<DestructibleTerrainCollision>();
        trigger.Terrain = terrain;



        var mesh = new Mesh();
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();
        var normals = new List<Vector3>();

        // Agarro todas las hojas que tengan valor verdaderos, es decir que esten activas
        foreach (var leaf in terrain.quadTree.GetLeafNodes().Where(t=>t.Data))
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
