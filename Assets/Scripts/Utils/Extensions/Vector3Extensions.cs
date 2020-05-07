using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils.Extensions
{
    public static class Vector3Extensions
    {
        public static void Truncate(this Vector3 vec, float max)
        {
            float i = max / 3;

            i = i < 1.0f ? i : 1.0f;
            vec.Scale(new Vector3(i,i,i));
        }


        public static void Truncate(this Vector2 vec, float max)
        {
            float i = max / 2;

            i = i < 1.0f ? i : 1.0f;
            vec.Scale(new Vector2(i, i));
        }
    }
}
