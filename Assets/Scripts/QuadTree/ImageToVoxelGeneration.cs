using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageToVoxelGeneration : MonoBehaviour
{
    public Texture2D image;
    public DestructibleTerrain terrain;
    public float threshold = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }



    // Update is called once per frame
    void Update()
    {
        
    } 
    
    private void Generate()
    {
        int cells = (int) Mathf.Pow(2,terrain.depth);

        for(int x = 0; x <= cells; x++)
        {
            for(int y = 0; y <= cells; y++)
            {
                Vector2 position = terrain.transform.position;
                position.x += ((x - (cells / 2)) / (float)cells) * terrain.size;
                position.y += ((y - (cells / 2)) / (float)cells) * terrain.size;

                var pixel = image.GetPixelBilinear(x / (float)cells, y / (float)cells);
                
                if(pixel.a > threshold)
                {
                    terrain.quadTree.Insert(position, true);
                }
            }
        }
    }
}
