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

    public Vector2 WorldUnitsInCamera = new Vector2();
    public Vector2 WorldToPixelAmount = new Vector2();

    //public GameObject Camera;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;

       // Camera = UnityEngine.Camera.main.gameObject;

        // Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
        // WorldUnitsInCamera.y = Camera.GetComponent<Camera>().orthographicSize * 2;
        //  WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;
        //
        //  WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        //  WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
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

    public Vector2 ConvertToWorldUnits(Vector2Int pixels)
    {
        Vector2 returnVec2 = new Vector2();

        // returnVec2.x = ((pixels.x / WorldToPixelAmount.x) - (WorldUnitsInCamera.x / 2)) +
        //                Camera.transform.position.x;
        // returnVec2.y = ((pixels.y / WorldToPixelAmount.y) - (WorldUnitsInCamera.y / 2)) +
        //                Camera.transform.position.y;

        return returnVec2;
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

        var width = voxelMaterial.mainTexture.width;
        var height = voxelMaterial.mainTexture.height;
        

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

            // uvs.Add(upperLeft);
            // uvs.Add(upperLeft + Vector3.right * leaf.Size);
            // uvs.Add(upperLeft + Vector3.down * leaf.Size);;
            // uvs.Add(upperLeft + Vector3.down * leaf.Size + Vector3.right * leaf.Size);
            //

            uvs.Add(new Vector2(0.4f, 0.5f));
            uvs.Add(new Vector2(0.5f, 0.5f));
            uvs.Add(new Vector2(0.5f, 0.5f));
            uvs.Add(new Vector2(0.5f, 0.5f));
            uvs.Add(new Vector2(0.5f, 0.5f));
            // uvs.Add(new Vector2(1,0));
            // uvs.Add(new Vector2(1, 1)); ;
            // uvs.Add(new Vector2(0, 1));


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


        float height = terrain.size;
        float width = terrain.size;


        // Agarro todas las hojas que tengan valor verdaderos, es decir que esten activas
        foreach (var leaf in terrain.quadTree.GetLeafNodes().Where(t=>t.Data))
        {
            var upperLeft = new Vector3(leaf.Position.x - leaf.Size * 0.5f, leaf.Position.y + leaf.Size * 0.5f, 0);
            var upperLeftLocal = new Vector3(width/2 + leaf.LocalPosition.x - leaf.Size * 0.5f, (height/2) +leaf.LocalPosition.y + leaf.Size * 0.5f, 0);

            var initialIndex = vertices.Count;

            // Creo los vertices de mi cuadrado

            vertices.Add(upperLeft);
            vertices.Add(upperLeft + Vector3.right * leaf.Size);
            vertices.Add(upperLeft + Vector3.down * leaf.Size); ;
            vertices.Add(upperLeft + Vector3.down * leaf.Size + Vector3.right * leaf.Size);


            // creo los UVS
            uvs.Add(new Vector3(upperLeftLocal.x / width, upperLeftLocal.y / height));

            var a = upperLeftLocal + Vector3.right * leaf.Size;
            a.x /= width;
            a.y /= height;
            uvs.Add(a);

            a = upperLeftLocal + Vector3.down * leaf.Size;
            a.x /= width;
            a.y /= height;
            uvs.Add(a); ;

            a = upperLeftLocal + Vector3.down * leaf.Size + Vector3.right * leaf.Size;
            a.x /= width;
            a.y /= height;
            uvs.Add(a);

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
