using UnityEngine;
using System.Collections;
using Assets.Scripts.SharedDataModels;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine.UI;

/*
 *
 */

public class GameController : MonoBehaviour
{
    public BasePanel PausePanel;
    public BasePanel GameOverPanel;

    public Slider SpaceshipHealthBar;
    public Slider WorldHealthBar;

    public EnemyGenerator EnemyManager;
    public TextMeshProUGUI EnemiesLeft;

    public Image WorldImage;

    public Ship SpaceShip;
    public World World;
    

    private SharedLevelData levelData;

    public void Start()
    {
        PausePanel.onOpen = t => { PauseGame(); };
        PausePanel.onClose = t => { ResumeGame(); };

        GameOverPanel.onOpen = t => { PauseGame(); };

        levelData = LocalStorage.GetObject<SharedLevelData>("levelData");

        SpaceshipHealthBar.maxValue = SpaceShip.maxLife;
        SpaceshipHealthBar.minValue = 0;

        WorldHealthBar.maxValue = levelData.WorldLife;
        WorldHealthBar.minValue = 0;
        WorldImage.sprite = levelData.WorldSprite;

    }



    public void Update()
    {
        UpdateSpaceShipHealthBar();
        UpdateWorldHealthBar();
        UpdateEnemiesLeft();
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

        if (SpaceShip.life <= 0)
        {
            GameOverPanel.OpenPanel();
        }


    }
    private void UpdateWorldHealthBar()
    {
        WorldHealthBar.value = World.Life;

        if (World.Life <= 0)
        {
            GameOverPanel.OpenPanel();
        }
    }

    private int enemiesLeft = -1;
    private void UpdateEnemiesLeft()
    {
        if (enemiesLeft != EnemyManager.EnemiesLeft())
        {
            enemiesLeft = EnemyManager.EnemiesLeft();

            EnemiesLeft.text = enemiesLeft + " Enemies Left";
        }

    }
}


