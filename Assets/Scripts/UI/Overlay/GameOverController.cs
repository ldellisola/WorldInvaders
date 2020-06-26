using Assets.Scripts.Ads;
using Assets.Scripts.Enemies;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Overlay
{
    public class GameOverController : MonoBehaviour
    {
        public BasePanel gameOverPanel;
        public GameController GameController;
        public AdManager AdManager;
        public Button ReviveButton;

        private bool WasRevived
        {
            get => LocalStorage.GetBool("was_revived");
            set => LocalStorage.SetBool("was_revived", value);
        }


        // Start is called before the first frame update
        void Start()
        {
            WasRevived = false;
            ReviveButton.enabled = true;
            gameOverPanel.SetOnOpen(t => GameController.EnablePauseButton(false));
            gameOverPanel.SetOnOpen(t => ReviveButton.interactable = !WasRevived);
            WasRevived = false;
            AdManager.RequestInterstitial();
            AdManager.RequestRewarded();

        }

        // Update is called once per frame
        void Update()
        {
            if (RestartGame)
            {
                GameController.EnablePauseButton(true);

                RestartGame = false;
                gameOverPanel.ClosePanel();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
            }

            if (GoToMenu)
            {
                GameController.EnablePauseButton(true);
                AmplitudeManager.LogOnGoBackToMenuOnGame();
                GoToMenu = false;
                SceneManager.LoadScene("MenuGUI",LoadSceneMode.Single);
            }

            if (Revive)
            {
                WasRevived = true;
                GameController.EnablePauseButton(true);
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
