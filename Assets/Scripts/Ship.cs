using Assets.Scripts.Misiles;
using Assets.Scripts.UI.Overlay;
using UnityEngine;

namespace Assets.Scripts
{
    public class Ship : MonoBehaviour
    {
        private FireMisile cannons = null;


        public float maxLife = 200;


        public float life { get; private set; }

        public void Awake()
        {
            cannons = GetComponent<FireMisile>();
        }

        // Start is called before the first frame update
        void Start()
        {
            life = maxLife;
        }

        void OnTriggerEnter2D(Collider2D obj)
        {

            if (obj.gameObject.TryGetComponent(out Misile misile))
            {
                if (misile.Data.Shooter == MisileData.Type.Enemy)
                {

                    life -= misile.damage;
                    misile.Explode();
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                cannons.Fire();
        }


        public void ResetLife()
        {
            life = maxLife;
        }


    }
}
