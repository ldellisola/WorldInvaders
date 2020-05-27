using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class LocalStorage : PlayerPrefs
    {
        public static void SetObject<T>(string key,T o)
        {
            string ser = JsonUtility.ToJson(o);
            SetString(key,ser);

            Save();
        }

        public static T GetObject<T>(string key)
        {
            string value = GetString(key);
            T  obj = JsonUtility.FromJson<T>(value);


            return obj;
        }

    }
}
