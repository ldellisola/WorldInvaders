using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Data;
using Assets.Scripts.Perks;
using UnityEngine;

namespace Assets.Scripts.Pools
{
    public class PerkPool : MonoBehaviour
    {
        public GameObject PerkPrefab;
        public Transform PerkTransform;

        private ObjectPool<Perk, Perk.Data> pool;


        public void Awake()
        {
            pool = new ObjectPool<Perk, Perk.Data>(PerkTransform,PerkPrefab);
        }

        public void Add(Perk.Data data)
        {
            pool.Add(data).transform.parent = this.transform;
        }

        public void DropPerk(Vector2 pos)
        {
            var data = new Perk.Data
            {
                type = Random.value > 0.5f ? Type.Healing : Type.Weapon,
                initialPosition = pos
            };

            Add(data);
        }

        
    }
}
