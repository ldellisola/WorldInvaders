using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularControls : MonoBehaviour
{
    public float RotateSpeed = 2f;
    public float RotateawaySpeed = 2f;
    public float Radius = 2.4f;



    public Vector2 rotation;
    public GameObject _centre;
    private float _angle;


    private void Start()
    {
        
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _angle += RotateSpeed * Time.deltaTime;

            var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            transform.position = _centre.transform.position + offset;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            _angle -= RotateSpeed * Time.deltaTime;
            var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            transform.position = _centre.transform.position + offset;
        }


        Vector2 direction = transform.position- _centre.transform.position;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);

        transform.rotation = rotation;




    }
}
