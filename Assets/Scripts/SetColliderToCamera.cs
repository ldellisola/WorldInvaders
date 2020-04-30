﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliderToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var border = Camera.main.ScreenToWorldPoint(new Vector3((float) Camera.main.pixelWidth, (float) Camera.main.pixelHeight));
        BoxCollider2D collider;


        GameObject leftWall = new GameObject("Left Wall");
        leftWall.layer = 8; //World Border
        leftWall.transform.parent = this.transform;
        leftWall.transform.localPosition = new Vector2(-border.x, 0);

        collider = leftWall.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0,2 * border.y);
        collider.isTrigger = true;


        GameObject rightWall = new GameObject("Right Wall");
        rightWall.layer = 8; //World Border
        rightWall.transform.parent = this.transform;
        rightWall.transform.localPosition = new Vector2(border.x, 0);

        collider = rightWall.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0, 2 * border.y);
        collider.isTrigger = true;


        GameObject topWall = new GameObject("Top Wall");
        topWall.layer = 8; //World Border
        topWall.transform.parent = this.transform;
        topWall.transform.localPosition = new Vector2(0, border.y);

        collider = topWall.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(2 * border.x,0);
        collider.isTrigger = true;


        GameObject bottomWall = new GameObject("Bottom Wall");
        bottomWall.layer = 8; //World Border
        bottomWall.transform.parent = this.transform;
        bottomWall.transform.localPosition = new Vector2(0,-border.y);

        collider = bottomWall.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(2 * border.x, 0);
        collider.isTrigger = true;


    }
}
