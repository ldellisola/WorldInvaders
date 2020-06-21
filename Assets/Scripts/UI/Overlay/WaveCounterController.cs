using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI.Overlay;
using TMPro;
using UnityEngine;

public class WaveCounterController : MonoBehaviour
{
    public GameController Game;
    public TextMeshProUGUI Text;
    void Start()
    {
        if (!Game.IsUnlimitedMode)
        {
            this.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        Text.SetText("WAVE " + Game.CurrentWave);
    }
}
