using System.Collections.Generic;
using Assets.Scripts.Ads;
using Assets.Scripts.UI.DataModels;
using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class LevelSelectorController : MonoBehaviour
    {
        public BasePanel LevelSelectorPanel;
        public BasePanel MainMenuPanel;
        public List<LevelData> levels;
        public AdManager AdManager;

        public void Start()
        {
            AdManager.RequestBanner();
            var temp = LevelSelectorPanel.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            temp.SetActive(false);

            foreach (var level in levels)
            {
                var newLevel = GameObject.Instantiate(temp, temp.transform.parent);
                    
                newLevel.GetComponent<LevelButton>().Initialize(level);
            }
            AdManager.RunBannerAd();

        }

        public void ButtonClick_GoBack()
        {
            AmplitudeManager.LogOnReturnToMenuFromLevels();
            LevelSelectorPanel.ClosePanel();
            MainMenuPanel.OpenPanel();

        }


    }
}
