using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies.Data;
using Assets.Scripts.Pools;
using Assets.Scripts.SharedDataModels;
using Assets.Scripts.UI.Overlay;
using Assets.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    public class EnemyGenerator : MonoBehaviour
    {
        public GameController GameController;
        public PoolManager Pools;
        public List<BaseEnemyData> Enemies;
        public float outerRim = 10;

        private EnemyWaveInstantiator WaveInstantiator { get; set; }

        private int i = 0;
        private List<Vector2> initPos = new List<Vector2>();

        public SharedLevelData.LevelMode LevelMode { get; private set; }


        public List<Tuple<BaseEnemyData, float>> Divers { get; set; } = new List<Tuple<BaseEnemyData, float>>();
        private int killedDivers { get; set; } = 0;
        private int StartDivers { get; set; } = 0;
        public List<Tuple<BaseEnemyData, float>> Wanderers { get; set; } = new List<Tuple<BaseEnemyData, float>>();
        private int killedWanderers { get; set; } = 0;
        private int StartWanderers { get; set; } = 0;
        public List<Tuple<BaseEnemyData, float>> FleeAndArrival { get; set; } = new List<Tuple<BaseEnemyData, float>>();
        private int killedFlees { get; set; } = 0;
        private int StartFlees { get; set; } = 0;


        public int CurrentWaveNumber => WaveInstantiator.Wave -1;


        void Start()
        {
            WaveInstantiator = new EnemyWaveInstantiator();

            var levelData = LocalStorage.GetObject<SharedLevelData>("levelData");

            LevelMode = levelData.isUnlimited ? SharedLevelData.LevelMode.Unlimited : SharedLevelData.LevelMode.Regular;
            Enemies = levelData.Enemies;

            if (LevelMode == SharedLevelData.LevelMode.Regular)
            {
                SetUpRegularMode();
            }
            else
            {
                GenerateWave();
            }

        }

        private void GenerateWave()
        {
            float timeDelta = Time.deltaTime;
            foreach (var enemy in Enemies)
            {
                enemy.AddTime(timeDelta);
                if (WaveInstantiator.HasToBeCreated(enemy))
                {
                    timer = 0;
                    foreach (var time in enemy.SpawnOrder)
                    {
                        var tupla = new Tuple<BaseEnemyData, float>(enemy, time);

                        switch (enemy.movementStyle)
                        {
                            case EnemyMovementStyle.Diver:
                                Divers.Add(tupla);
                                StartDivers++;
                                break;
                            case EnemyMovementStyle.FleeAndArrival:
                                FleeAndArrival.Add(tupla);
                                StartFlees++;
                                break;
                            case EnemyMovementStyle.Wander:
                                Wanderers.Add(tupla);
                                StartWanderers++;
                                break;
                        }
                    }
                }
            }
        }

        private void SetUpRegularMode()
        {
            Enemies.ForEach(t => t.ResetTimer());

            foreach (var enemy in Enemies)
            {
                foreach (var time in enemy.SpawnOrder)
                {
                    var tupla = new Tuple<BaseEnemyData, float>(enemy, time);

                    switch (enemy.movementStyle)
                    {
                        case EnemyMovementStyle.Diver:
                            Divers.Add(tupla);
                            StartDivers = enemy.SpawnOrder.Count;
                            break;
                        case EnemyMovementStyle.FleeAndArrival:
                            FleeAndArrival.Add(tupla);
                            StartFlees = enemy.SpawnOrder.Count;
                            break;
                        case EnemyMovementStyle.Wander:
                            Wanderers.Add(tupla);
                            StartWanderers = enemy.SpawnOrder.Count;
                            break;
                    }
                }
            }
        }

        private float timer = 0;
        void Update()
        {
            
            if (!GameController.IsGamePaused)
            {
                if (LevelMode == SharedLevelData.LevelMode.Unlimited)
                {
                    GenerateWave();
                }

                timer += Time.deltaTime;

                var DiverCreations = Divers.FindAll(t => t.Item2 <= timer);
                DiverCreations.ForEach(t =>
                {
                    t.Item1.initialPositon = CalculatePosition();
                    Pools.EnemyPool.Add(t.Item1);
                });
                Divers.RemoveAll(t => t.Item2 <= timer);

                var FleesCreations = FleeAndArrival.FindAll(t => t.Item2 <= timer);
                FleesCreations.ForEach(t =>
                {
                    t.Item1.initialPositon = CalculatePosition();
                    Pools.EnemyPool.Add(t.Item1);
                });
                FleeAndArrival.RemoveAll(t => t.Item2 <= timer);

                var WandererCreations = Wanderers.FindAll(t => t.Item2 <= timer);
                WandererCreations.ForEach(t =>
                {
                    t.Item1.initialPositon = CalculatePosition();
                    Pools.EnemyPool.Add(t.Item1);
                });
                Wanderers.RemoveAll(t => t.Item2 <= timer);

            }

        }

        public void OnDrawGizmos()
        {
            initPos.ForEach(t =>
            {
                Gizmos.DrawSphere(t, 0.5f);
            });
        }

        public void NotifyEnemyKilled(EnemyMovementStyle dataMovementStyle)
        {
            switch (dataMovementStyle)
            {
                case EnemyMovementStyle.Diver:
                    killedDivers++;
                    break;
                case EnemyMovementStyle.FleeAndArrival:
                    killedFlees++;
                    break;
                case EnemyMovementStyle.Wander:
                    killedWanderers++;
                    break;
            }
        }

        public bool GameWon()
        {
            if (LevelMode == SharedLevelData.LevelMode.Unlimited)
            {
                return false;
            }

            return EnemiesLeft() == 0;
        }

        public int EnemiesLeft()
        {
            return StartWanderers + StartFlees + StartDivers - (killedWanderers + killedFlees + killedDivers);
        }

        private Vector2 CalculatePosition()
        {
            Vector2 pos;
            do
            {
                pos = Random.onUnitSphere;

                pos.y = Mathf.Abs(pos.y);
            } while (pos.y < 0.5);



            return pos * outerRim * 3;
        }


    }
}
