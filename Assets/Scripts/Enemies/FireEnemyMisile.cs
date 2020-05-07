﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemyMisile : MonoBehaviour
{
    public MisileData misileData;
    public PoolManager PoolManager;
    public bool enable = true;

    public Vector3 aim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {


        if(enable)
            PoolManager.MisilePool.pool.Add(new MisileData(misileData, transform.position,(aim - transform.position),MisileData.Type.Enemy));

    }
}
