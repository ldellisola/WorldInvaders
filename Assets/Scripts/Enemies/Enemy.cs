using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies.MovementStyle;
using UnityEngine;

public class Enemy : MonoBehaviour, IPooledObject<BaseEnemyData>
{
    public PoolManager pools;


    public BaseEnemyData data {get; private set; }
    private SpriteRenderer sp;
    private float life = 0;
    private IMovementStyle movement = null;

    public void Awake()
    {
        GameObject child = new GameObject("EnemySprite");
        child.transform.parent = this.transform;


        child.transform.localScale = new Vector3(3.7f, 3.7f);

        sp = child.AddComponent<SpriteRenderer>();
        sp.sortingLayerName = "ShipsLayer";
    }

    public void Initialize(BaseEnemyData data)
    {
        this.transform.position = data.initialPositon;
        this.data = data;

        life = data.life;
        sp.sprite = data.sprite;

        switch (data.movementStyle)
        {
            case EnemyMovementStyle.FleeAndArrival:
                movement = new FleeAndArrival();
                break;
            case EnemyMovementStyle.PursuitAndEvade:
                movement = new PursuitAndEvade();
                break;
            case EnemyMovementStyle.Diver:
                movement = new Diver();
                break;
            case EnemyMovementStyle.Wander:
                movement = new Wander();
                break;
        }

    }


    public void Update()
    {
        
        movement.Update(data,this);

    }



    void OnTriggerEnter2D(Collider2D obj)
    {
        print("Colision Nave");

       // pools.EnemyPool.explosionPool.Add(new EnemyExplosion.Data(this.transform.position));

       // this.gameObject.SetActive(false);
    }
    
}
