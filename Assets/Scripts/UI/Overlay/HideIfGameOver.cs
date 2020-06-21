using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Overlay
{
    public class HideIfGameOver : MonoBehaviour
    {

        public GameController GameController;

        public Button btnRevive;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (GameController.IsGameLost)
            {
                btnRevive.gameObject.SetActive(true);
            }
            else
            {
                btnRevive.gameObject.SetActive(false);

            }
        }
    }
}
