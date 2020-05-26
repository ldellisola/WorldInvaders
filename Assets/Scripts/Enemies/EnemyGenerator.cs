using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public PoolManager Pools;
    public List<BaseEnemyData> Enemies;
    public float outerRim = 10;

    private int i = 0;
    private List<Vector2> initPos = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        //Enemies.ForEach(t=>t.Initialize(initPos[i++]));
    }

    // Update is called once per frame
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
