using Assets.Scripts.Enemies.Data;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Enemies
{
    public class EnemyWaveInstantiator
    {
        private float baseWaveDuration = 15;
        private float AddedWaveDuration = 2;

        private Random rand = new Random();

        public int Wave => (_wave / 3);

        private int _wave = 3;



        public void GenerateWave(BaseEnemyData data)
        {

            

            int MonstersToSpawn = Wave;

            data.SpawnOrder.Clear();

            float totalWaveDuration = baseWaveDuration + Wave * AddedWaveDuration;

            data.WaveDuration = totalWaveDuration;

            for (int i = 0; i < MonstersToSpawn; i++)
            {
                data.SpawnOrder.Add((float) rand.NextDouble() * (totalWaveDuration- AddedWaveDuration));
            }
            Debug.Log("Created " + MonstersToSpawn +" enemies");
            _wave++;

            data.SpawnOrderIndex = 0;

        }

        public bool HasToBeCreated(BaseEnemyData data)
        {
            if ( Wave == 1 || data.timer > data.WaveDuration )
            {
                data.ResetTimer();
                GenerateWave(data);

                return true;
            }

            return false;
        }
    }
}