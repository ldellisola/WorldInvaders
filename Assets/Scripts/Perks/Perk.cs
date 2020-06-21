using System;
using System.Runtime.CompilerServices;
using Assets.Scripts.Misiles;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Perks
{
    public enum Type
    {
        Healing,
        Weapon
    }
    public class Perk : MonoBehaviour, IPooledObject<Perk.Data>
    {
        public Sprite WeaponSprite;
        public Sprite HealSprite;

        public ShockwaveScript Shockwave;

        private Rigidbody2D Body;
        private SpriteRenderer SpriteRenderer;
        private bool Disappearing = false;

        void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            this.data = new Data()
            {
                type = Type.Weapon
            };
            transform.localScale = Vector3.one * 2;
        }

        public Data data { get; private set; }
        public void Initialize(Data data)
        {
            transform.position = data.initialPosition;
            this.data = data;

            Vector2 direction = -transform.position;

            direction.Normalize();

            Body.AddForce(direction,ForceMode2D.Impulse);

            switch (data.type)
            {
                case Type.Healing:
                    SpriteRenderer.sprite = HealSprite;
                    break;
                case Type.Weapon:
                    SpriteRenderer.sprite = WeaponSprite;
                    break;
            }

            Disappearing = false;
            SpriteRenderer.color = new Color(SpriteRenderer.color.r,SpriteRenderer.color.g,SpriteRenderer.color.b,1);

            
        }

        void OnTriggerEnter2D(Collider2D obj)
        {

            if (obj.gameObject.CompareTag("Player-Planet-Orbit"))
            {
                Body.velocity = Vector2.zero;
                Body.angularVelocity = 0;
            }

            if (obj.gameObject.TryGetComponent(out Ship spaceShip))
            {
                ApplyPerk(spaceShip);
                // Animacion desaparecer
                RunDisappearAnimation();
            }
        }

        public void Update()
        {
            if (Disappearing)
            {
                Color col = new Color(SpriteRenderer.color.r,SpriteRenderer.color.g,SpriteRenderer.color.b,SpriteRenderer.color.a-0.1f);
                SpriteRenderer.color = col;

                if (SpriteRenderer.color.a <= 0)
                {
                    gameObject.SetActive(false);
                    SpriteRenderer.color = new Color(SpriteRenderer.color.r,SpriteRenderer.color.g,SpriteRenderer.color.b,1);

                }
            }
        }

        private void RunDisappearAnimation()
        {
            Disappearing = true;
        }

        protected void ApplyPerk(Ship spaceShip)
        {
            switch (data.type)
            {
                case Type.Healing:
                    GameStats.PerkHealth++;
                    spaceShip.ResetLife();
                    break;
                case Type.Weapon:
                    GameStats.PerkEnergyBlast++;
                    Shockwave.GenerateWave();
                    break;
            }
        }

        
        public class Data
        {
            public Vector2 initialPosition;
            public Type type;
        }

    
    }
}
