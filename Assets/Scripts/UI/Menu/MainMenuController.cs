using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {

        public BasePanel LevelSelectorPanel;
        public BasePanel MainMenuPanel;
        public BasePanel SettingsPanel;




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
    }
}
