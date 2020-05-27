using System;
using System.Collections.Generic;
using Assets.Scripts.SharedDataModels;
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


        public SharedLevelData GenerateSharedData()
        {
            return new SharedLevelData
            {
                Name = Name,
                Enemies = Enemies,
                WorldLife = WorldLife,
                WorldSprite = WorldSprite
            };
        }


    }
}
