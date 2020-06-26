using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Misiles;
using Assets.Scripts.Pools;
using Assets.Scripts.UI.Overlay;
using UnityEngine;

public class FireMisile: MonoBehaviour
{
    public Transform MisileStartPosition;
    public PoolManager PoolManager;
    public float ShootingMovementThreshold = 0.001f;
    public float ShootingUpdateTime = 1f;
    public MisileData misileData;
    public GameController GameController;


    public bool enable = true;

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
        if (!GameController.IsGamePaused)
        {
            isMoving = (LastPosition - transform.position).sqrMagnitude > ShootingMovementThreshold;

            LastPosition = transform.position;

            isMoving = false;

            if (!isMoving)
            {
                timeCounter += Time.deltaTime;

                if (timeCounter >= ShootingUpdateTime)
                {
                    timeCounter -= ShootingUpdateTime;
                    Fire();
                }
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //    Fire();
        }
    }

    public void Fire()
    {
        if(enable)
            PoolManager.MisilePool.Add(new MisileData(misileData, MisileStartPosition.position,
                MisileStartPosition.position,MisileData.Type.Player));

    }
}
