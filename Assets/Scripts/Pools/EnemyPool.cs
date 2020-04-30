using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Transform enemyPoolTransform;
    public GameObject enemyPrefab;

    public GameObject explosionPrefab;

    public ObjectPool<Enemy, BaseEnemyData> pool;
    public ObjectPool<EnemyExplosion, EnemyExplosion.Data> explosionPool;


    public void Awake()
    {
        pool = new ObjectPool<Enemy, BaseEnemyData>(enemyPoolTransform, enemyPrefab);
        explosionPool = new ObjectPool<EnemyExplosion, EnemyExplosion.Data>(enemyPoolTransform, explosionPrefab);
    }
}
