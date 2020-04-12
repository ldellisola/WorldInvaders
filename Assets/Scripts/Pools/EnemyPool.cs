using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Transform enemyPoolTransform;
    public GameObject enemyPrefab;

    public GameObject explosionPrefab;

    public ObjectPool<Enemy, Enemy.Data> pool;
    public ObjectPool<EnemyExplosion, EnemyExplosion.Data> explosionPool;


    public void Awake()
    {
        pool = new ObjectPool<Enemy, Enemy.Data>(enemyPoolTransform, enemyPrefab);
        explosionPool = new ObjectPool<EnemyExplosion, EnemyExplosion.Data>(enemyPoolTransform, explosionPrefab);
    }
}
