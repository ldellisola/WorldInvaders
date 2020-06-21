using Assets.Scripts.Ads;
using Assets.Scripts.UI.DataModels;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LevelButton : MonoBehaviour
    {
        private LevelData data;


        public void Initialize(LevelData data)
        {
            this.data = data;
            gameObject.name = data.Name;

            transform.Find("LevelImage").GetComponent<Image>().sprite = data.WorldSprite;
            transform.Find("LevelName").GetComponentInChildren<TextMeshProUGUI>().text = data.Name;
            
            this.gameObject.SetActive(true);
        }

        public void ButtonClick_SelectLevel()
        {
            LocalStorage.SetObject("levelData",data.GenerateSharedData());
            AmplitudeManager.LogOnSelectedLevel(data.Name);
            GetComponentInParent<BasePanel>().ClosePanel();
            SceneManager.LoadScene(sceneName: "SampleLevel",LoadSceneMode.Single);
        }
    }
}
