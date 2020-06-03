using Assets.Scripts.Misiles;
using UnityEngine;

namespace Assets.Scripts.Pools
{
    public class MisilePool : MonoBehaviour
    {
        public Transform MisilePoolTransform;
        public GameObject misilePrefab;

        public GameObject ExplosionPrefab;

        private ObjectPool<Misile, MisileData> pool;
        private ObjectPool<MisileExplosion, MisileExplosion.Data> ExplosionsPool;


        public void Awake()
        {
            pool = new ObjectPool<Misile, MisileData>(MisilePoolTransform,misilePrefab);
            ExplosionsPool = new ObjectPool<MisileExplosion, MisileExplosion.Data>(MisilePoolTransform, ExplosionPrefab);
        }

        public void Add(MisileData data)
        {
            pool.Add(data).transform.parent = this.transform;
        }

        public void Add(MisileExplosion.Data data)
        {
            ExplosionsPool.Add(data).transform.parent = this.transform;
        }
    }
}
