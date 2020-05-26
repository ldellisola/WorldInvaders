using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SharedDataModels
{
    [Serializable]
    public class SharedLevelData
    {
        public Sprite WorldSprite;
        public string Name;

        public List<BaseEnemyData> Enemies;
        public float WorldLife;
    }
}
