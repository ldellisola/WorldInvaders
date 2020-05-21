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

        private CanvasGroup canvasGroup;

        public void Awake()
        {

            canvasGroup = this.GetComponent<CanvasGroup>();

            canvasGroup.alpha = initialAlpha;
        }



        public void OpenPanel()
        {
            this.gameObject.SetActive(true);
            canvasGroup.interactable = true;
            LeanTween.alphaCanvas(this.canvasGroup, 1, 1);
            //LeanTween.alpha(this.gameObject, 1, 1).setOnStart(()=> this.gameObject.SetActive(true));
            onOpen.Invoke(gameObject);
        }


        public void ClosePanel()
        {
            canvasGroup.interactable = false;

            LeanTween.alphaCanvas(this.canvasGroup, 0, 1).setOnComplete((t)=> this.gameObject.SetActive(false));

            onClose.Invoke(gameObject);

        }

    }
}
