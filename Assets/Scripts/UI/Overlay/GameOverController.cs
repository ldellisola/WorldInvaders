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
            if (RestartGame)
            {
                RestartGame = false;
                gameOverPanel.ClosePanel();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
            }

            if (GoToMenu)
            {
                AmplitudeManager.LogOnGoBackToMenuOnGame();
                GoToMenu = false;
                SceneManager.LoadScene("MenuGUI",LoadSceneMode.Single);
            }

            if (Revive)
            {
                gameOverPanel.ClosePanel();

                GameController.ResumeGame();
                Revive = false;
                GameController.ResetPlayerLife();
            }
        }

        private bool RestartGame { get; set; } = false;
        public void ButtonClick_TryAgain()
        {
            AdManager.RunIntertitialAd((sender, args) =>
            {
                Debug.Log("INTERTITAL AD ENDED");
                RestartGame = true;
            });
            
        }

        private bool GoToMenu { get; set; } = false;
        public void ButtonClick_MainMenu()
        {
            AdManager.RunIntertitialAd((sender, args) =>
            {
                GoToMenu = true;
            });
        }

        private bool Revive { get; set; } = false;
        public void ButtonClick_Revive()
        {
            AdManager.RunRewardedAd((sender, reward) =>
            {
                Debug.Log("REWARDED AD POST");

                Debug.Log("REWARDED AD RESETED LIFE");


                Revive = true;

                Debug.Log("REWARDED AD CONTINUE");

            });
        }

    }
}
