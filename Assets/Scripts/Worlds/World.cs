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
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {

    }
   
}
