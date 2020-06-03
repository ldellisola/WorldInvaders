using Assets.Scripts.Ads;
using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class MainMenuController : MonoBehaviour
    {

        public BasePanel LevelSelectorPanel;
        public BasePanel MainMenuPanel;
        public BasePanel SettingsPanel;

        public AdManager AdManager;
        public PurchaseManager PurchaseManager;

        public void Start()
        {           
            AdManager.RequestBanner();
            AdManager.RunBannerAd();
            // AdManager.RequestInterstitial();
        }

        public void Update()
        {
            
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

        public void ButtonClick_BuyAds()
        {
            PurchaseManager.BuyNoAds();
        }
    }
}
