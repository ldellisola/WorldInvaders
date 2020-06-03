using Assets.Scripts.Misiles;
using Assets.Scripts.Pools;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class FireEnemyMisile : MonoBehaviour
    {
        public MisileData misileData;
        public PoolManager PoolManager;
        public bool enable = true;

        public Vector3 aim;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Fire()
        {
            if(enable)
                PoolManager.MisilePool.Add(new MisileData(misileData, transform.position,(aim - transform.position),MisileData.Type.Enemy));

        }
    }
}
