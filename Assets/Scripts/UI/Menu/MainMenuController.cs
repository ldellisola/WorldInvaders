using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {

        public BasePanel LevelSelectorPanel;
        public BasePanel MainMenuPanel;
        public BasePanel SettingsPanel;

        public AdManager AdManager;

        public void Start()
        {
            AdManager.RequestInterstitial();
        }
        


        public void ButtonClick_Play()
        {
            LevelSelectorPanel.OpenPanel();
            MainMenuPanel.ClosePanel();
        }

        public void ButtonClick_Settings()
        {
            SettingsPanel.OpenPanel();
            MainMenuPanel.ClosePanel();
        }

        public void ButtonClick_ShowAd()
        {
            AdManager.RunIntertitialAd();
            AdManager.RequestInterstitial();

        }
    }
}
