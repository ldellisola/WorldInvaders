using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMisile: MonoBehaviour
{
    public Transform MisileStartPosition;

    public PoolManager PoolManager;

    public float ShootingMovementThreshold = 0.001f;
    public float ShootingUpdateTime = 1f;

    private Vector3 LastPosition;
    private bool isMoving = false;

    private float timeCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        LastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = (LastPosition - transform.position).sqrMagnitude > ShootingMovementThreshold;

        LastPosition = transform.position;

        if (!isMoving)
        {
            timeCounter += Time.deltaTime;

            if (timeCounter >= ShootingUpdateTime)
            {
                timeCounter -= ShootingUpdateTime;
                Fire();
            }
        }
        else
        {
            timeCounter = 0;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //    Fire();
    }

    private void Fire()
    {

        PoolManager.MisilePool.pool.Add(new Misile.Data(MisileStartPosition.position));

    }
}
