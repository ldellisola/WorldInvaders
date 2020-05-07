﻿using System;
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

                hit = Physics2D.Raycast(a,Vector2.left, MinDistanceToBorder, LayerMask.GetMask("World Border"));
                Debug.DrawRay(position, notNormDir, Color.magenta, 10);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.DrawRay(a, Vector2.left, Color.cyan, 10);
                    continue;
                }


                hit = Physics2D.Raycast(a,Vector2.right, MinDistanceToBorder, LayerMask.GetMask("World Border"));
                Debug.DrawRay(position, notNormDir, Color.magenta, 10);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.DrawRay(a, Vector2.right, Color.cyan, 10);
                    continue;
                }


                hit = Physics2D.Raycast(a,Vector2.up, MinDistanceToBorder, LayerMask.GetMask("World Border"));
                Debug.DrawRay(position, notNormDir, Color.magenta, 10);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.DrawRay(a, Vector2.up, Color.cyan, 10);
                    continue;
                }


                hit = Physics2D.Raycast(a,Vector2.down, MinDistanceToBorder, LayerMask.GetMask("World Border"));
                Debug.DrawRay(position, notNormDir, Color.magenta, 10);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.DrawRay(a, Vector2.down, Color.cyan, 10);
                    continue;
                }



            } while (hit.collider != null);

            
            //Debug.DrawRay(a,a + new Vector3(1,0), Color.red, 10);
            //Debug.DrawRay(a,a + new Vector3(1,1), Color.red, 10);
            //Debug.DrawRay(a,a + new Vector3(0,1), Color.red, 10);
            //Debug.DrawRay(a,a + new Vector3(1.5f,0), Color.red, 10);
            //Debug.DrawRay(a,a + new Vector3(1.5f,1), Color.red,10);

            //Debug.DrawRay(position, position + new Vector3(1, 0), Color.green, 10);
            //Debug.DrawRay(position, position + new Vector3(1, 1), Color.green, 10);
            //Debug.DrawRay(position, position + new Vector3(0, 1), Color.green, 10);
            //Debug.DrawRay(position, position + new Vector3(1.5f, 0), Color.green, 10);
            //Debug.DrawRay(position, position + new Vector3(1.5f, 1), Color.green, 10);


            //Debug.DrawRay(position, a - position, Color.white, 10);



            return a;
        }
    }
}
