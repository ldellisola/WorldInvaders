using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData/MisileData", order = 1)]
public class BaseEnemyMisileData : ScriptableObject
{
    public Sprite sprite;
    public float range;
    public float timeAlive;
    public float speed;
    public float damage;
}
