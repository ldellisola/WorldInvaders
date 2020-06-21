using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enemies.Data;
using Assets.Scripts.UI.DataModels;
using UnityEngine;

namespace Assets.Scripts.SharedDataModels
{
    [Serializable]
    public class SharedLevelData
    {
        public enum LevelMode
        {
            Regular,
            Unlimited
        }

        public Sprite WorldSprite;
        public string Name;

        public List<BaseEnemyData> Enemies;
        public float WorldLife;
        public bool isUnlimited;
    }
}
