using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Misiles;
using UnityEngine;

namespace Assets.Scripts.Enemies.Data
{
    public enum EnemyMovementStyle
    {
        Diver,
        FleeAndArrival,
        Wander,
        PursuitAndEvade
    }

    [CreateAssetMenu(menuName = "EnemyData",order = 1)]
    public class BaseEnemyData : ScriptableObject
    {
        public Vector2 initialPositon;
        public MisileData misile;
        public float velocity;
        public EnemyMovementStyle movementStyle;
        public Sprite sprite;
        public float life;
        public float mass;

        public List<float> SpawnOrder;
        internal int SpawnOrderIndex { get; set; }= 0;
        internal float timer { get; private set; } = 0;

        internal float WaveDuration { get; set; } = 0;

        public void Initialize(Vector2 pos)
        {
            initialPositon = pos;
        }

        public void AddTime(float time)
        {
            timer += time;
        }

        public bool FinishedCreatingEnemies() => (SpawnOrderIndex >= SpawnOrder.Count);


        public bool HasToBeCreated()
        {
            if (SpawnOrderIndex >= SpawnOrder.Count)
                return false;

            if (timer > SpawnOrder[SpawnOrderIndex])
            {
                SpawnOrderIndex++;
                return true;
            }
            return false;
        }

        public void ResetTimer()
        {
            timer = 0;
            SpawnOrderIndex = 0;
        }

    }
}