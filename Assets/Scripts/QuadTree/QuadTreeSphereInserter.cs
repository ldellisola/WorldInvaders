using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeSphereInserter : MonoBehaviour
{

    public DestructibleTerrain terrain;
    public float radius = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var insertionPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        // click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Deleting: " + insertionPoint);
            terrain.quadTree.Insert(insertionPoint.origin,radius, false);
        } // Click derecho
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Inserting: " + insertionPoint);

            terrain.quadTree.Insert(insertionPoint.origin,radius, true);
        } 
    }
}
