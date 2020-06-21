using UnityEngine;

namespace Assets.Scripts.Misiles
{
    public class MisileExplosion : MonoBehaviour, IPooledObject<MisileExplosion.Data>
    {
        public class Data
        {
            public Vector2 pos;
            public float scale;
            public Data(Vector2 pos, float scale)
            {
                this.pos = pos;
                this.scale = scale;
            }
        }

        public SpriteAnimator spriteAnimator;

        public void Initialize(Data data)
        {
            this.transform.position = data.pos;
            this.transform.localScale = Vector3.one * data.scale;
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
