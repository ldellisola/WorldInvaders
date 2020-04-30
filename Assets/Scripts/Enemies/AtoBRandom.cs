using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtoBRandom : MonoBehaviour
{
    public Transform StartPoint;
    public Vector3 EndPoint;

    private System.Random rnd = new System.Random();

    public int NewDirectionInterval = 1;

    private float time = 1;

    private float _angle = 0;
    private Vector3 CurrentDirection;


    // Use this for initialization
    void Start()
    {
        //StartPoint.position = new Vector3(-4f, 9, 0);
        EndPoint = new Vector3(0, 0, 0);
    }



    // Update is called once per frame
    void Update()
    {
        var data = GetComponent<Enemy>().data;

        time += Time.deltaTime;
        if (time >= NewDirectionInterval )
        {
            StartPoint = transform;
            CurrentDirection = CalculateDirection(data);
            time = 0;
        }
        transform.Translate(CurrentDirection * Time.smoothDeltaTime);
    }

    private Vector3 CalculateDirection(BaseEnemyData data)
    {

        var exactDirection = (EndPoint - StartPoint.position);

        exactDirection.Normalize();



        int r = rnd.Next(12);

        if(r > 8)
        {
            exactDirection = Vector3.right * rnd.Next(-1, 1) * data.velocity;
        }
        else if(r > 5)
        {
            exactDirection = Vector3.up *rnd.Next(-1, 1) * data.velocity;

        }else if(r > 3)
        {
            exactDirection = Vector3.zero;
        }
        exactDirection.Normalize();


        return exactDirection;
    }


}
