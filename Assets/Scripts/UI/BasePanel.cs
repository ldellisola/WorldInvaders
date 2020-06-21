using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BasePanel : MonoBehaviour
    {
        public Action<GameObject> onOpen { private get; set; } = t => { };
        public Action<GameObject> onClose { private get; set; } = t => { };

        public float initialAlpha;
        public float FadeTime = 0.2f;
        private CanvasGroup canvasGroup;

        public void Awake()
        {

            canvasGroup = this.GetComponent<CanvasGroup>();

            canvasGroup.alpha = initialAlpha;
        }



        public void OpenPanel()
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
            this.gameObject.SetActive(true);
            canvasGroup.interactable = true;
            LeanTween.alphaCanvas(this.canvasGroup, 1, FadeTime);
            onOpen.Invoke(gameObject);
            //LeanTween.alpha(this.gameObject, 1, 1).setOnStart(()=> this.gameObject.SetActive(true));
            // onOpen.Invoke(gameObject);
        }


        public void ClosePanel()
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;

            onClose.Invoke(gameObject);

            LeanTween.alphaCanvas(this.canvasGroup, 0, FadeTime).setOnComplete((t)=> this.gameObject.SetActive(false));

            // onClose.Invoke(gameObject);

        }

    }
}
