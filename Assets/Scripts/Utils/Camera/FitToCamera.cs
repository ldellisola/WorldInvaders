using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        var border = Camera.main.ScreenToWorldPoint(new Vector3((float)Camera.main.pixelWidth, (float)Camera.main.pixelHeight));


        var sp = GetComponent<SpriteRenderer>();
        border.z = 0;
        sp.size = border * 2;

    }
}
