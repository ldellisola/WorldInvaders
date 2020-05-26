using System.Collections.Generic;
using Assets.Scripts.UI.DataModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LevelSelectorController : MonoBehaviour
    {
        public BasePanel LevelSelectorPanel;
        public BasePanel MainMenuPanel;
        public List<LevelData> levels;

        public void Start()
        {
            LevelSelectorPanel.onOpen = (go) =>
            {
                var temp = go.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                temp.SetActive(false);

                foreach (var level in levels)
                {
                    var newLevel = GameObject.Instantiate(temp, temp.transform.parent);
                    
                    newLevel.GetComponent<LevelButton>().Initialize(level);
                }
            };
        }

        public void ButtonClick_GoBack()
        {
            LevelSelectorPanel.ClosePanel();
            MainMenuPanel.OpenPanel();

        }


    }
}
