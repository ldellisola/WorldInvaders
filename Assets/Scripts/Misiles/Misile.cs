using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misile : MonoBehaviour, IPooledObject<Misile.Data>
{

    public class Data
    {
        public Vector2 pos;

        public Data(Vector2 pos)
        {
            this.pos = pos;
        }
    }

    public Rigidbody2D Body;
    public float speed;
    public float maxLifeTime;
    private float lifetime = 0;

    public float Radius => GetComponent<CircleCollider2D>().radius / 100;


    public float ExplosionRadius { get; private set; } = 0.2f;

    public PoolManager PoolManager;


    public void Initialize(Data data)
    {
        this.transform.position = data.pos;
        lifetime = 0;
        Body.AddForce(transform.position * speed , ForceMode2D.Force);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;

        // Preguntar por que desaparece

        if (lifetime >= maxLifeTime)
        {
            PoolManager.MisilePool.ExplosionsPool.Add(new MisileExplosion.Data(this.transform.position));
            gameObject.SetActive(false);
        }
    }




    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    print("Colision");
        
    //}


    public void Explode()
    {
        PoolManager.MisilePool.ExplosionsPool.Add(new MisileExplosion.Data(this.transform.position));

        this.gameObject.SetActive(false);
    }
}
