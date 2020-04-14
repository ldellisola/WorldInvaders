using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreePencil : MonoBehaviour
{
    public DestructibleTerrain terrain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var insertionPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        // click izquierdo
        if (Input.GetMouseButton(0))
        {
            terrain.quadTree.Insert(insertionPoint.origin, false);
        } // Click derecho
        else if (Input.GetMouseButton(1))
        {

            terrain.quadTree.Insert(insertionPoint.origin, true);
        }
    
    }
}
