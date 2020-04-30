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
        private const float HorizontalMargin = 2;

        private const float MaxTimeWithTarget = 5;

        private const float VelMultiplier = 0.5f;
        private const float MassMultiplier = 1;
        private const float MaxVelMultiplier = 1;
        public void Update(BaseEnemyData data, MonoBehaviour enemy)
        {
            if (timeSinceNewTarget == 0 || (enemy.transform.position - target).sqrMagnitude < 0.5 * 0.5)
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

        private Vector3 GenerateNewTarget(Vector3 position)
        {
            Vector3 newT = Random.insideUnitCircle;
            newT.y *= VerticalMargin;
            newT.x *= HorizontalMargin;

            var a = position + newT;
            a.z = 0;

            return a;
        }
    }
}
