using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.Misiles;
using Assets.Scripts.SharedDataModels;
using Assets.Scripts.UI.DataModels;
using Assets.Scripts.Utils;
using UnityEngine;

public class World : MonoBehaviour
{
    public WorldData Data;

    private float MaxLife => Data.life;

    public float Life { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        var levelData = LocalStorage.GetObject<SharedLevelData>("levelData");

        Data.sprite = levelData.WorldSprite;
        Data.life = levelData.WorldLife;

        var sp = GetComponent<SpriteRenderer>();
        sp.sprite = Data.sprite;
        Life = MaxLife;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Misile mis) && mis.Data.Shooter == MisileData.Type.Enemy)
        {
            Life -= mis.damage;
            GameStats.DamagePlanet += mis.damage;
            mis.Explode();
        }

        if (collider.TryGetComponent(out Enemy enemy))
        {
            Life -= enemy.CollisionDamage;
            GameStats.DamagePlanet += enemy.CollisionDamage;
            enemy.Explode();
        }
    }

    public void Update()
    {
        // Hacer algo cuando la vida llega a 0
    }

    public void ResetLife()
    {
        Life = MaxLife;
    }
}
