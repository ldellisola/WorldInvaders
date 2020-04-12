using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject EasyEnemy;
    public int easyEnemySpawnInterval = 4;
    public Vector3 easyEnemySpainPoint;
    private float easyEnemySpawnTimer = 0;
    public PoolManager Pools;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        easyEnemySpawnTimer += Time.deltaTime;
        if(easyEnemySpawnTimer >= easyEnemySpawnInterval)
        {
            easyEnemySpawnTimer = 0;
            print("New Enemy!");
            Vector2 vector = new Vector2(Random.Range(2, 4) * (Random.value > 0.5 ? -1 : 1), Random.Range(3, 9) * (Random.value > 0.5 ? -1 : 1));

            Pools.EnemyPool.pool.Add(new Enemy.Data(vector));
            
        }
        
    }
}
