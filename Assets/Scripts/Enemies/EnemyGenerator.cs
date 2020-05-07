using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public PoolManager Pools;
    public List<BaseEnemyData> Enemies;

    private int i = 0;
    private List<Vector2> initPos = new List<Vector2>{new Vector2(0,-9),new Vector2(5,9), new Vector2(-5,-9)};

    // Start is called before the first frame update
    void Start()
    {
        Enemies.ForEach(t=>t.Initialize(initPos[i++]));
    }

    private bool created = false;
    // Update is called once per frame
    void Update()
    {
        if(!created)
            Enemies.ForEach(t =>
            {
                t.AddTime(Time.deltaTime);
                if (t.HasToBeCreated())
                {
                    Pools.EnemyPool.pool.Add(t);
                    created = true;
                }
            });

    }
}
