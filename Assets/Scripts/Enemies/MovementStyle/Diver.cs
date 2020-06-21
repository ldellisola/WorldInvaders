using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enemies.Data;
using Assets.Scripts.Utils.Extensions;
using UnityEngine;

namespace Assets.Scripts.Enemies.MovementStyle
{
    // Physics.Raycast y poner caja externa.
    /// <summary>
    ///  Based on the Seek strategy, this movement style will try to dive as fast as possible to the planet
    ///
    /// https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-seek--gamedev-849
    /// </summary>
    public class Diver: IMovementStyle
    {
        private Vector3 target = Vector3.zero;

        public Diver()
        {
        }

        public Diver(Vector2 targ)
        {
            target = targ;
        }

        public void Update(BaseEnemyData data, MonoBehaviour enemy, float? overwriteSpeed = null,  float? overwriteMass = null)
        {
            enemy.transform.Translate(
                Update(target,
                    enemy.transform.position,
                    enemy.transform.forward,
                    overwriteSpeed ?? data.velocity,
                    2 * overwriteSpeed ?? data.velocity,
                    overwriteMass ?? data.mass
                ));
        }


        public void DrawGizmos(BaseEnemyData data, MonoBehaviour enemy)
        {
        }


        public Vector3 Update(Vector2 target, Vector2 position, Vector2 currentDirection, float speed,float maxSpeed, float mass)
        {
            var desiredDirection = (target - position);
            desiredDirection.Normalize();
            currentDirection.Normalize();

            var currentVelocity = currentDirection * speed;
            var desiredVelocity = desiredDirection * speed;
            var steering = (desiredVelocity - currentVelocity);

            steering.Truncate(maxSpeed);

            steering /= mass;

            var definitiveDirection = steering + currentVelocity;
            definitiveDirection.Truncate(maxSpeed);

            return definitiveDirection;
        }
    }
}
