using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPooledObject<Enemy.Data>
{
    public PoolManager pools;
    public class Data
    {
        public Vector2 initialPosition;

        public Data(Vector2 pos)
        {
            initialPosition = pos;
        }
    }

    public void Initialize(Data data)
    {
        this.transform.position = data.initialPosition;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        print("Colision Nave");

        pools.EnemyPool.explosionPool.Add(new EnemyExplosion.Data(this.transform.position));

        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
