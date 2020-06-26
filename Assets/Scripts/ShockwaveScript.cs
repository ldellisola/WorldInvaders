﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.Misiles;
using Assets.Scripts.UI.Overlay;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{
    private bool isRunning = false;
    public GameController GameController;
    public AudioSource AudioSource;

    public float maxSize = 6;
	void Awake ()
    {
        AudioSource = GetComponent<AudioSource>();
    }
	
	
	void Update ()
    {
        if (isRunning && !GameController.IsGamePaused)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            if (transform.localScale.x > maxSize)
            {
                transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

                isRunning = false;
                gameObject.SetActive(false);

            }
        }

    }

    public void GenerateWave(Vector2 pos)
    {
        this.transform.position = pos;
        gameObject.SetActive(true);
        AudioSource.Play();
        isRunning = true;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (isRunning)
        {
            if (obj.TryGetComponent(out Enemy enemy))
            {
                enemy.Explode();
            }

            if (obj.TryGetComponent(out Misile misile) && misile.Data.Shooter == MisileData.Type.Enemy)
            {
                misile.Explode();
            }
        }
    }



}
    