using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.Ads
{
    public partial class AdManager: MonoBehaviour
    {
        private BannerView banner;


        public void RunBannerAd()
        {
            if(EnableAds)
                this.banner.Show();
        }

        public void RequestBanner()
        {
            if (EnableAds)
            {
#if UNITY_ANDROID
                string adUnitId =
                     "ca-app-pub-9853268330632359/7656821213"; // "ca-app-pub-3940256099942544/6300978111"; // <--TEST
#else
        string adUnitId = "unexpected_platform";
#endif
                banner = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);



                AdRequest request = new AdRequest.Builder()
                    // .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
                    .Build();
                this.banner.LoadAd(request);
            }
        }
    }
}
