using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {

        public BasePanel LevelSelectorPanel;
        public BasePanel MainMenuPanel;




        public void ButtonClick_Play()
        {
            LevelSelectorPanel.OpenPanel();
            MainMenuPanel.ClosePanel();
        }
    }
}
