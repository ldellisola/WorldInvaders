using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public float spawnInterval;
    private float timer = 0;

    public void Initialize(Vector2 pos)
    {
        initialPositon = pos;
    }

    public void AddTime(float time)
    {
        timer += time;
    }

    public bool HasToBeCreated()
    {
        if (timer > spawnInterval)
        {
            timer -= spawnInterval;
            return true;
        }

        return false;
    }

}
