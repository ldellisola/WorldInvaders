using UnityEngine;

namespace Assets.Scripts.Misiles
{
    [CreateAssetMenu(menuName = "MisileData", order = 1)]
    public class MisileData : ScriptableObject
    {
        public enum Type
        {
            Player,
            Enemy
        }
        public MisileData(MisileData o,Vector2 position, Vector2 direction,Type Shooter)
        {
            this.position = position;
            sprite = o.sprite;
            range = o.range;
            damage = o.damage;
            timeAlive = o.timeAlive;
            speed = o.speed;
            this.direction = direction;
            this.direction.Normalize();
            this.Shooter = Shooter;
            this.ShootingSound = o.ShootingSound;
        }

        public Sprite sprite;
        public float range;
        public float timeAlive;
        public float speed;
        public float damage;

        public AudioClip ShootingSound;

        public Vector2 direction { get; set; }
        public Type Shooter { get; set; }

        public Vector3 position { get; private set; }
    }
}
