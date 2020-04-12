using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisilePool : MonoBehaviour
{
    public Transform MisilePoolTransform;
    public GameObject misilePrefab;

    public GameObject ExplosionPrefab;

    public ObjectPool<Misile, Misile.Data> pool;
    public ObjectPool<MisileExplosion, MisileExplosion.Data> ExplosionsPool;


    public void Awake()
    {
        print(misilePrefab);
        pool = new ObjectPool<Misile, Misile.Data>(MisilePoolTransform,misilePrefab);
        ExplosionsPool = new ObjectPool<MisileExplosion, MisileExplosion.Data>(MisilePoolTransform, ExplosionPrefab);
    }
}
