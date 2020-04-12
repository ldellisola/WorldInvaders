using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMisile: MonoBehaviour
{
    public Transform MisileStartPosition;

    public PoolManager PoolManager;

   

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

        PoolManager.MisilePool.pool.Add(new Misile.Data(MisileStartPosition.position));

    }
}
