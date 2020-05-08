using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public WorldData Data;

    private float MaxLife => Data.life;

    private float currentLife = 0;

    // Start is called before the first frame update
    void Start()
    {
        var sp = GetComponent<SpriteRenderer>();
        sp.sprite = Data.sprite;
        currentLife = MaxLife;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Misile mis) && mis.Data.Shooter == MisileData.Type.Enemy)
        {
            currentLife -= mis.damage;
            
        }
    }

    public void Update()
    {
        // Hacer algo cuando la vida llega a 0
    }
   
}
