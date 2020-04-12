using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float totalAnimationTime;

    private int spriteIndex;
    private float elapsedTime;
    private float animationTimePerFrame;

    public bool loops;

    public bool IsPlaying { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationTimePerFrame = totalAnimationTime / sprites.Length;
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= animationTimePerFrame)
            {
                ShowNextSprite();
                elapsedTime -= animationTimePerFrame;
            }
        }

        
    }

    private void ShowNextSprite()
    {
        spriteIndex = (1 + spriteIndex) % sprites.Length;

        if (!loops && spriteIndex == 0)
        {
            print("APAGO animacion");
            IsPlaying = false;
        }
        else
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }

    }

    public void Play()
    {
        this.spriteIndex = 0;
        elapsedTime = 0;
        IsPlaying = true;
    }
}
