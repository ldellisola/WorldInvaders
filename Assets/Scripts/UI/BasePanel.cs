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
        private List<Action<GameObject>> onOpen { get; set; } = new List<Action<GameObject>>();
        private List<Action<GameObject>> onClose { get; set; } = new List<Action<GameObject>>();
        

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
            onOpen.ForEach(t=>t.Invoke(gameObject));
            //LeanTween.alpha(this.gameObject, 1, 1).setOnStart(()=> this.gameObject.SetActive(true));
            // onOpen.Invoke(gameObject);
        }

        public void SetOnOpen(Action<GameObject> func)
        {
            onOpen.Add(func);
        }

        public void SetOnCose(Action<GameObject> func)
        {
            onClose.Add(func);
        }

        public void ClosePanel()
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;

            onClose.ForEach(t=>t.Invoke(gameObject));

            LeanTween.alphaCanvas(this.canvasGroup, 0, FadeTime).setOnComplete((t)=> this.gameObject.SetActive(false));

            // onClose.Invoke(gameObject);

        }

    }
}
