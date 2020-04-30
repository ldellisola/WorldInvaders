using Assets.Scripts.Utils.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies.MovementStyle
{
    public class FleeAndArrival: IMovementStyle
    {
        private enum State
        {
            Wondwering,
            Diving,
            Shooting,
            Retreating
        }
        private State state = State.Wondwering;
        private float timeSinceStartedState = 0;
        private bool hasShooted = false;
        private Vector2 retreatTarget;
        private Vector3 target = Vector3.zero;

        private Wander w =new Wander();

        private const float outerRadius = 8;
        private const float innerRadius = 4;
        private const float TimeToShoot = 0.5f;
        private const float VelMultiplier = 1;
        private const float MassMultiplier = 1;
        private const float MaxVelMultiplier = 2;

        public void Update(BaseEnemyData data, MonoBehaviour enemy)
        {
            state = SwitchState(data, enemy);
            switch (state)
            {
                case State.Wondwering:
                    Wander(data, enemy);
                    break;
                case State.Diving:
                    Dive(data, enemy);
                    break;
                case State.Shooting:
                    Shoot(data, enemy);
                    break;
                case State.Retreating:
                    Retreat(data, enemy);
                    break;
            }
        }

        

        private State SwitchState(BaseEnemyData data, MonoBehaviour enemy)
        {
            State newState = state;
            timeSinceStartedState += Time.deltaTime;


            switch (state)
            {
                case State.Wondwering:
                    if ((enemy.transform.position - target).sqrMagnitude <= outerRadius* outerRadius || timeSinceStartedState > 5)
                    {
                        newState = State.Diving;
                        timeSinceStartedState = 0;
                        Debug.Log("DIVING");
                    }

                    // Implement timer
                    break;
                case State.Diving:
                    if ((enemy.transform.position - target).sqrMagnitude <= innerRadius * innerRadius)
                    {
                        newState = State.Shooting;
                        Debug.Log("SHOOTING");

                    }

                    hasShooted = false;
                    break;
                case State.Shooting:
                    if (timeSinceStartedState > 2 * TimeToShoot)
                    {
                        newState = State.Retreating;

                        retreatTarget = CalculateRetreateTarget(enemy.transform);
                        timeSinceStartedState = 0;
                        Debug.Log("RETIEVING");

                    }

                    break;
                case State.Retreating:
                    if ((enemy.transform.position - target).sqrMagnitude > Mathf.Pow(outerRadius + Random.value * 2, 2))
                    {
                        newState = State.Wondwering;
                        Debug.Log("WONDERING");

                    }

                    break;
            }

            if (state != newState)
            {
                timeSinceStartedState = 0;
            }

            return newState;
        }

        private Vector3 CalculateRetreateTarget(Transform enemy)
        {
            Vector3 newVec = target-enemy.position;

            newVec.y = -newVec.y;
            return 4 * newVec;


            //if (enemy.forward.x >= 0 && enemy.forward.y >= 0)
            //{
            //    newVec.x = enemy.forward.x;
            //    newVec.y = -enemy.forward.y;
            //}
            //else if (enemy.forward.x >= 0 && enemy.forward.y < 0)
            //{
            //    newVec.x = enemy.forward.x;
            //    newVec.y = -enemy.forward.y;
            //}
            //else if (enemy.forward.x < 0 && enemy.forward.y >= 0)
            //{
            //    newVec.x = enemy.forward.x;
            //    newVec.y = -enemy.forward.y;
            //}
            //else// (enemy.forward.x < 0 && enemy.forward.y < 0)
            //{
            //    newVec.x = enemy.forward.x;
            //    newVec.y = -enemy.forward.y;
            //}


        }

        private void Wander(BaseEnemyData data, MonoBehaviour enemy)
        {
            w.Update(data,enemy);
        }

        private void Dive(BaseEnemyData data, MonoBehaviour enemy)
        {
            var direction = new Diver().Update(
                target,enemy.transform.position,enemy.transform.forward,data.velocity * VelMultiplier
                ,data.velocity * MaxVelMultiplier,data.mass * MassMultiplier);

            enemy.transform.Translate(direction);
        }


        private void Shoot(BaseEnemyData data, MonoBehaviour enemy)
        {
            if (!hasShooted && timeSinceStartedState > TimeToShoot)
            {
               // enemy.GetComponent<FireEnemyMisile>().Fire();
                hasShooted = true;
            }
        }

        private void Retreat(BaseEnemyData data, MonoBehaviour enemy)
        {
            var currentDirection = enemy.transform.forward;
            currentDirection.Normalize();

            Vector2 position = enemy.transform.position;

            Vector2 desiredDirection = (retreatTarget - position);
            desiredDirection.Normalize();

            Vector2 currentVelocity = currentDirection * data.velocity;
            Vector2 desiredVelocity = desiredDirection * data.velocity;
            Vector2 steering = (desiredVelocity - currentVelocity);

            steering.Truncate(2 * data.velocity);

            steering /= data.mass;

            var definitiveDirection = steering + currentVelocity;
            definitiveDirection.Truncate(2 * data.velocity);

            enemy.transform.Translate(definitiveDirection);
        }
    }
}
