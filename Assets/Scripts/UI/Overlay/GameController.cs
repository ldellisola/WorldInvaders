using UnityEngine;
using System.Collections;
using Assets.Scripts.SharedDataModels;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public BasePanel PausePanel;
    public Slider SpaceshipHealthBar;
    public Slider WorldHealthBar;

    public Ship SpaceShip;
    public World World;
    

    private SharedLevelData levelData;

    public void Start()
    {
        PausePanel.onOpen = t => { PauseGame(); };
        PausePanel.onClose = t => { ResumeGame(); };

        levelData = LocalStorage.GetObject<SharedLevelData>("levelData");

        SpaceshipHealthBar.maxValue = SpaceShip.maxLife;
        SpaceshipHealthBar.minValue = 0;

        WorldHealthBar.maxValue = levelData.WorldLife;
        WorldHealthBar.minValue = 0;

    }



    public void Update()
    {
        UpdateSpaceShipHealthBar();
        UpdateWorldHealthBar();
    }











    public void ButtonClick_OpenPauseMenu()
    {
        PausePanel.OpenPanel();
    }

    public void ButtonClick_ClosePauseMenu()
    {
        PausePanel.ClosePanel();
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


    private void UpdateSpaceShipHealthBar()
    {

        SpaceshipHealthBar.value = SpaceShip.life;


    }
    private void UpdateWorldHealthBar()
    {
        WorldHealthBar.value = World.Life;
    }
}


