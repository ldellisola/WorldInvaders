using UnityEngine;

namespace Assets.Scripts.Misiles
{
    public class MisileExplosion : MonoBehaviour, IPooledObject<MisileExplosion.Data>
    {
        public class Data
        {
            public Vector2 pos;

            public Data(Vector2 pos)
            {
                this.pos = pos;
            }
        }

        public SpriteAnimator spriteAnimator;

        public void Initialize(Data data)
        {
            this.transform.position = data.pos;
            spriteAnimator.Play();
        }



        // Update is called once per frame
        void Update()
        {
            if (!spriteAnimator.IsPlaying)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
