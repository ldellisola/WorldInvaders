using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtoBRandom : MonoBehaviour
{
    public float Speed = 0.5f;
    public Transform StartPoint;
    public Vector3 EndPoint;

    private System.Random rnd = new System.Random();

    public int NewDirectionInterval = 1;

    private float time = 0;

    private float _angle = 0;

    private Vector3 CurrentDirection;

    // Use this for initialization
    void Start()
    {
        //StartPoint.position = new Vector3(-4f, 9, 0);
        EndPoint = new Vector3(0, 0, 0);
        CurrentDirection = CalculateDirection();
    }



    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        //print(time);
        if (time >= NewDirectionInterval )
        {
            StartPoint = transform;
            CurrentDirection = CalculateDirection();
            //CurrentDirection.Normalize();
            print(CurrentDirection);
            time = 0;

        }
        transform.Translate(CurrentDirection * Time.smoothDeltaTime);

        //Vector2 direction = transform.position - EndPoint;

        //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);

        //transform.rotation = rotation;
    }

    private Vector3 CalculateDirection()
    {

        var exactDirection = (EndPoint - StartPoint.position);

        exactDirection.Normalize();



        int r = rnd.Next(12);

        if(r > 8)
        {
            exactDirection = Vector3.right * rnd.Next(-1, 1) * Speed;
        }
        else if(r > 5)
        {
            exactDirection = Vector3.up *rnd.Next(-1, 1) * Speed;

        }else if(r > 3)
        {
            exactDirection = Vector3.zero;
        }
        exactDirection.Normalize();


        return exactDirection;
    }


}
