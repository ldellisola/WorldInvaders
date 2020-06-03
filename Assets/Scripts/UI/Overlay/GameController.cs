using Assets.Scripts.Ads;
using Assets.Scripts.Enemies;
using Assets.Scripts.SharedDataModels;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Overlay
{
    public class GameController : MonoBehaviour
    {
        public BasePanel PausePanel;
        public BasePanel GameOverPanel;

        public AdManager AdManager;

        public Slider SpaceshipHealthBar;
        public Slider WorldHealthBar;

        public EnemyGenerator EnemyManager;
        public TextMeshProUGUI EnemiesLeft;

        public Image WorldImage;

        public Ship SpaceShip;

        public World World;

        public bool IsGamePaused { get; private set; }
    

        private SharedLevelData levelData;

        public void Start()
        {
            AdManager.RequestBanner();

            PausePanel.onOpen = t => { PauseGame(); };
            PausePanel.onClose = t => { ResumeGame(); };

            GameOverPanel.onOpen = t => { PauseGame(); };
            GameOverPanel.onClose = t => { ResumeGame(); };

            levelData = LocalStorage.GetObject<SharedLevelData>("levelData");

            SpaceshipHealthBar.maxValue = SpaceShip.maxLife;
            SpaceshipHealthBar.minValue = 0;

            WorldHealthBar.maxValue = levelData.WorldLife;
            WorldHealthBar.minValue = 0;
            WorldImage.sprite = levelData.WorldSprite;

            AdManager.RunBannerAd();

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
            IsGamePaused = true;
        }

        public void ResumeGame()
        {
            IsGamePaused = false;
        }

        public void ResetPlayerLife()
        {
            SpaceShip.ResetLife();
            World.ResetLife();
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
}


