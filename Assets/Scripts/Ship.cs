using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public GameObject BulletPrefab;
    public Transform BulletStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
    }

    public void Fire()
    {
        var Bullet = Instantiate(BulletPrefab) as GameObject;
        Bullet.transform.position = BulletStartPosition.position;
    }
}
