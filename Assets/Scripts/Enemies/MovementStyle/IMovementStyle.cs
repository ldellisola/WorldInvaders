using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enemies.Data;
using UnityEngine;

namespace Assets.Scripts.Enemies.MovementStyle
{
    interface IMovementStyle
    {
        void Update(BaseEnemyData data, MonoBehaviour enemy, float? overwriteSpeed = null, float? overwriteMass = null);

        void DrawGizmos(BaseEnemyData data, MonoBehaviour enemy);
    }
}
