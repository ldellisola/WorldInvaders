using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SharedDataModels;
using Assets.Scripts.UI.DataModels;
using Assets.Scripts.Utils;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public PoolManager Pools;
    public List<BaseEnemyData> Enemies;
    public float outerRim = 10;

    private int i = 0;
    private List<Vector2> initPos = new List<Vector2>();

    void Start()
    {
        var levelData = LocalStorage.GetObject<SharedLevelData>("LevelData");

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
                    initPos.Add(t.initialPositon);
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
