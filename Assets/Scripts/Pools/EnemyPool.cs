using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Transform enemyPoolTransform;
    public GameObject enemyPrefab;

    public GameObject explosionPrefab;

    private ObjectPool<Enemy, BaseEnemyData> pool;
    private ObjectPool<EnemyExplosion, EnemyExplosion.Data> explosionPool;


    public void Awake()
    {
        pool = new ObjectPool<Enemy, BaseEnemyData>(enemyPoolTransform, enemyPrefab);
        explosionPool = new ObjectPool<EnemyExplosion, EnemyExplosion.Data>(enemyPoolTransform, explosionPrefab);
    }

    public void Add(BaseEnemyData data)
    {
        pool.Add(data).transform.parent = this.transform;
    }

    public void Add(EnemyExplosion.Data data)
    {
        explosionPool.Add(data).transform.parent = this.transform;
    }
}
