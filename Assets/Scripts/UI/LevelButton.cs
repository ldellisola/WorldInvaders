using Assets.Scripts.UI.DataModels;
using TMPro;
using UnityEngine;
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
    }
}
