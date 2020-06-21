using Assets.Scripts.Pools;
using Assets.Scripts.UI.Overlay;
using UnityEngine;

namespace Assets.Scripts.Misiles
{
    public class Misile : MonoBehaviour, IPooledObject<MisileData>
    {
    
        public Rigidbody2D Body;
        public PoolManager PoolManager;
        public GameController GameController;

        public MisileData Data { get; private set; }


        public float speed => Data.speed;
        public float maxLifeTime => Data.timeAlive;
        public float maxTravelDistance => Data.range;
        public float damage => Data.damage;
        public float Radius => GetComponent<CircleCollider2D>().radius / 100;
        public float ExplosionRadius => damage / 100;


        private float lifetime = 0;
    
        public void Initialize(MisileData data)
        {
            this.transform.position = data.position;
            this.Data = data;
            lifetime = 0;
            data.direction.Normalize();
            Body.AddForce(data.direction * speed , ForceMode2D.Force);

            GetComponent<SpriteRenderer>().sprite = data.sprite;

            transform.localScale = new Vector3(data.scale,data.scale,data.scale);

            if (data.ShootingSound != null)
            {
                var audio = GetComponent<AudioSource>();

                audio.clip = data.ShootingSound;
                audio.Play();

            }
        }



        // Update is called once per frame
        void Update()
        {
            if (!GameController.IsGamePaused)
            {
                lifetime += Time.deltaTime;

                // Preguntar por que desaparece

                if (lifetime >= maxLifeTime || (transform.position - Data.position).sqrMagnitude >
                    maxTravelDistance * maxTravelDistance)
                {
                    PoolManager.MisilePool.Add(new MisileExplosion.Data(this.transform.position, Data.scale));
                    gameObject.SetActive(false);
                }
            }
        }




        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Misile mis))
            {
                if (mis.Data.Shooter != this.Data.Shooter)
                {
                    Explode();
                    gameObject.SetActive(false);
                }
            }

        }


        public void Explode()
        {
            PoolManager.MisilePool.Add(new MisileExplosion.Data(this.transform.position, Data.scale));

            this.gameObject.SetActive(false);
        }
    }
}
