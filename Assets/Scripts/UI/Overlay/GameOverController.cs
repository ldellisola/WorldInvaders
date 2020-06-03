using Assets.Scripts.Ads;
using Assets.Scripts.Enemies;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Overlay
{
    public class GameOverController : MonoBehaviour
    {
        public BasePanel gameOverPanel;
        public EnemyGenerator EnemyGenerator;

        public GameController GameController;

        public AdManager AdManager;


        // Start is called before the first frame update
        void Start()
        {
            AdManager.RequestInterstitial();
            AdManager.RequestRewarded();

        }

        // Update is called once per frame
        void Update()
        {

            if (EnemyGenerator.KiledAllEnemies())
            {
                gameOverPanel.OpenPanel();
            }
        }


        public void ButtonClick_TryAgain()
        {
            AdManager.RunIntertitialAd((sender, args) =>
            {
                Debug.Log("INTERTITAL AD ENDED");
                gameOverPanel.ClosePanel();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
            });
            
        }

        public void ButtonClick_MainMenu()
        {
            AdManager.RunIntertitialAd((sender, args) =>
            {
                SceneManager.LoadScene("MenuGUI",LoadSceneMode.Single);
            });
        }

        public void ButtonClick_Revive()
        {
            AdManager.RunRewardedAd((sender, reward) =>
            {
                Debug.Log("REWARDED AD POST");

                Debug.Log("REWARDED AD RESETED LIFE");


                GameController.ResetPlayerLife();



                Debug.Log("REWARDED AD PANEL STARTED CLOSING");

                gameOverPanel.ClosePanel();

                Debug.Log("REWARDED AD CONTINUE");

            });
        }

    }
}
