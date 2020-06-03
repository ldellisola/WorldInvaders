using Assets.Scripts.Ads;
using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class BuyAdsButton : MonoBehaviour
    {
        public AdManager AdManager;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (!AdManager.EnableAds)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
