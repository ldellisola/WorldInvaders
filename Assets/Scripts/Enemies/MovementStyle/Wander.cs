using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies.MovementStyle
{
    public class Wander:IMovementStyle
    {
        private float timeSinceNewTarget = 0;
        private Vector3 target;

        private const float VerticalMargin = 1;
        private const float HorizontalMargin = 6;

        private const float MaxTimeWithTarget = 2;

        private const float VelMultiplier = 0.5f;
        private const float MassMultiplier = 1;
        private const float MaxVelMultiplier = 1;
        private const float MinDistanceToBorder = 1f;
        public void Update(BaseEnemyData data, MonoBehaviour enemy)
        {
            if (timeSinceNewTarget == 0 || (enemy.transform.position - target).sqrMagnitude < 1)
            {
                target = GenerateNewTarget(enemy.transform.position);
                timeSinceNewTarget = 0;
            }

            timeSinceNewTarget += Time.deltaTime;

            var direction = new Diver().Update(
                target, enemy.transform.position, enemy.transform.forward, data.velocity * VelMultiplier
                , data.velocity * MaxVelMultiplier, data.mass * MassMultiplier);

            enemy.transform.Translate(direction);

            if (timeSinceNewTarget > MaxTimeWithTarget)
            {
                timeSinceNewTarget = 0;
            }

        }

        public void DrawGizmos(BaseEnemyData data, MonoBehaviour enemy)
        {
            var old = Gizmos.color;
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(target,0.3f);

            Gizmos.color = old;
        }

        private Vector3 GenerateNewTarget(Vector3 position)
        {
            Vector3 a;
            RaycastHit2D hit;
            do
            {
                Vector3 newT = Random.insideUnitCircle;
                newT.y *= VerticalMargin;
                newT.x *= HorizontalMargin;
                a = position + newT;

                a.z = 0;


                position.z = 0;
                var notNormDir = newT;
                newT.Normalize();

                float maxDistance = 30;

                hit = Physics2D.Raycast(a,Vector2.left, MinDistanceToBorder, LayerMask.GetMask("World Border"));
                Debug.DrawRay(position, notNormDir, Color.magenta, 10);

                if (hit.collider != null)
                {
                    Debug.DrawRay(a, Vector2.left, Color.cyan, 10);

                    hit = Physics2D.Raycast(a, Vector2.left, maxDistance, LayerMask.GetMask("World Border"));

                    if(hit.collider == null || hit.collider.gameObject.name != "Left Wall")
                        continue;
                }


                hit = Physics2D.Raycast(a,Vector2.right, MinDistanceToBorder, LayerMask.GetMask("World Border"));
                Debug.DrawRay(position, notNormDir, Color.magenta, 10);

                if (hit.collider != null)
                {
                    Debug.DrawRay(a, Vector2.right, Color.cyan, 10);

                    hit = Physics2D.Raycast(a, Vector2.right, maxDistance, LayerMask.GetMask("World Border"));

                    if (hit.collider == null || hit.collider.gameObject.name != "Right Wall")
                        continue;
                }


                hit = Physics2D.Raycast(a,Vector2.up, MinDistanceToBorder, LayerMask.GetMask("World Border"));
                Debug.DrawRay(position, notNormDir, Color.magenta, 10);

                if (hit.collider != null)
                {
                    Debug.DrawRay(a, Vector2.up, Color.cyan, 10);

                    hit = Physics2D.Raycast(a, Vector2.up, maxDistance, LayerMask.GetMask("World Border"));

                    if (hit.collider == null || hit.collider.gameObject.name != "Top Wall")
                        continue;
                }


                hit = Physics2D.Raycast(a,Vector2.down, MinDistanceToBorder, LayerMask.GetMask("World Border"));
                Debug.DrawRay(position, notNormDir, Color.magenta, 10);

                if (hit.collider != null)
                {
                    Debug.DrawRay(a, Vector2.down, Color.cyan, 10);

                    hit = Physics2D.Raycast(a, Vector2.down, maxDistance, LayerMask.GetMask("World Border"));

                    if (hit.collider == null || hit.collider.gameObject.name != "Bottom Wall")
                        continue;

                }

                



            } while (hit.collider != null);
            

            return a;
        }
    }
}
