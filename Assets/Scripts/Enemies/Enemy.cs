using System;
using Assets.Scripts.Enemies.Data;
using Assets.Scripts.Enemies.MovementStyle;
using Assets.Scripts.Misiles;
using Assets.Scripts.Pools;
using Assets.Scripts.UI.Overlay;
using Assets.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IPooledObject<BaseEnemyData>
    {
        public PoolManager pools;
        public GameController GameController;
        public float CollisionDamage => data.velocity * data.mass * 10;

        public BaseEnemyData data {get; private set; }
        private SpriteRenderer sp;
        private float life = 0;
        private IMovementStyle movement = null;
        private bool InZone = false;


        private bool dropsPerk = false;

        private Diver initalMovement = null;

        private float OutsideZonespeed = 1;
        private float OutsideZoneMass = 5;

        public EnemyGenerator EnemyManager;


        private ParticleSystem particleSystem;

        public void Awake()
        {
            GameObject child = new GameObject("EnemySprite");
            child.transform.parent = this.transform;


            child.transform.localScale = new Vector3(3.7f, 3.7f);

            particleSystem = GetComponent<ParticleSystem>();

            sp = child.AddComponent<SpriteRenderer>();
            sp.sortingLayerName = "ShipsLayer";
        }

        public void Initialize(BaseEnemyData data)
        {
            particleSystem.Stop();

            dropsPerk = false;
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

                    if (Random.value > 0.75)
                    {
                        dropsPerk = true;
                        particleSystem.Play();
                    }

                    break;
            }

            initalMovement = new Diver(new Vector2(0,10));
            
            InZone = false;

        }


        public void Update()
        {
            if (!GameController.IsGamePaused)
            {
                if (InZone)
                {
                    movement.Update(data, this);
                }
                else
                {
                    initalMovement.Update(data, this,OutsideZonespeed,OutsideZoneMass);

                    InZone = isInZone();
                }

                if (life <= 0)
                {
                    Explode();
                }
            }
        }

        private bool isInZone()
        {
            float distance = 30;

            Tuple<Vector2, string>[] directions =
            {
                Tuple.Create(Vector2.left, "Left Wall"), Tuple.Create(Vector2.right, "Right Wall"), Tuple.Create(Vector2.up, "Top Wall"),
                Tuple.Create(Vector2.down, "Bottom Wall")
            };

            foreach (var direction in directions)
            {
                var hit = Physics2D.Raycast(transform.position, direction.Item1, distance, LayerMask.GetMask("World Border"));

                if (hit.collider == null || hit.collider.gameObject.name != direction.Item2)
                    return false;
            }


            return true;
        }

        public void OnDrawGizmos()
        {
            movement.DrawGizmos(data,this);
        }


        void OnTriggerEnter2D(Collider2D obj)
        {

            if (obj.gameObject.TryGetComponent(out Misile misile) && InZone)
            {
                if (misile.Data.Shooter == MisileData.Type.Player)
                {
                    life -= misile.damage;
                    Debug.Log(String.Format("Le saco {0} de vida. Le queda {1}", misile.damage,life));

                    misile.Explode();

                }
            }
        }

        public void Explode()
        {
            EnemyManager.NotifyEnemyKilled(data.movementStyle);
            this.gameObject.SetActive(false);
            pools.EnemyPool.Add(new EnemyExplosion.Data(this.transform.position));

            if(dropsPerk)
                pools.PerkPool.DropPerk(transform.position);

            switch (data.movementStyle)
            {
                case EnemyMovementStyle.FleeAndArrival:
                    GameStats.KilledRegular++;
                    break;
                case EnemyMovementStyle.Diver:
                    GameStats.KilledDiver++;
                    break;
                case EnemyMovementStyle.Wander:
                    GameStats.KilledTank++;
                    break;
            }

        }
    }
}
