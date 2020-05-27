using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.SharedDataModels;
using Assets.Scripts.UI.DataModels;
using Assets.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    public PoolManager Pools;
    public List<BaseEnemyData> Enemies;
    public float outerRim = 10;

    private int i = 0;
    private List<Vector2> initPos = new List<Vector2>();
    private int killedEnemies = 0;

    void Start()
    {
        var levelData = LocalStorage.GetObject<SharedLevelData>("levelData");

        Enemies = levelData.Enemies;



        
    }

    void Update()
    {
            Enemies.ForEach(t =>
            {
                t.AddTime(Time.deltaTime);
                if (t.HasToBeCreated())
                {
                    t.initialPositon = CalculatePosition();
                    //initPos.Add(t.initialPositon);
                    Pools.EnemyPool.Add(t);
                }
            });

    }

    public void OnDrawGizmos()
    {
        initPos.ForEach(t =>
        {
            Gizmos.DrawSphere(t,0.5f);
        });
    }

    public void NotifyEnemyKilled()
    {
        killedEnemies++;
    }

    public bool KiledAllEnemies() => killedEnemies >= Enemies.Sum(t => t.SpawnOrder.Count);

    private Vector2 CalculatePosition()
    {
        Vector2 pos;
        do
        {
            pos = Random.onUnitSphere;

            pos.y = Mathf.Abs(pos.y);
        } while (pos.y < 0.5);



        return pos * outerRim*3;
    }


}
