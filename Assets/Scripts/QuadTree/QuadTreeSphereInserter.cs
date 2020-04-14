using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeSphereInserter : MonoBehaviour
{

    public DestructibleTerrain Terrain;
    public ArrayDestructibleTerrain ArrayTerrain;
    public float radius = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var insertionPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Terrain.isActiveAndEnabled)
        {
            // click izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                Terrain.quadTree.Insert(insertionPoint.origin, radius, false);
            } // Click derecho
            else if (Input.GetMouseButtonDown(1))
            {

                Terrain.quadTree.Insert(insertionPoint.origin, radius, true);
            }
        }
        else if (ArrayTerrain.isActiveAndEnabled)
        {
            // click izquierdo
            if (Input.GetMouseButtonDown(0))
            {
                ArrayTerrain.quadTree.Insert(insertionPoint.origin, radius, false);
            } // Click derecho
            else if (Input.GetMouseButtonDown(1))
            {

                ArrayTerrain.quadTree.Insert(insertionPoint.origin, radius, true);
            }
        }
    }
}
