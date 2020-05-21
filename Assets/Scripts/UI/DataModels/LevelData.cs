using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UI.DataModels
{
    [CreateAssetMenu(menuName = "LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        public Sprite WorldSprite;
        public string Name;

        public List<BaseEnemyData> Enemies;
        public float WorldLife;

    }
}
