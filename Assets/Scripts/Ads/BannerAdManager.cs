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
            #if DEBUG
                string adUnitId = "ca-app-pub-3940256099942544/6300978111";
            #else
                string adUnitId = "ca-app-pub-9853268330632359/7656821213";
            #endif

                banner = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);



                banner.OnAdLeavingApplication += BannerOnAdLeavingApplication;

                AdRequest request = new AdRequest.Builder()
            #if DEBUG
                    .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
            #endif
                    .Build();
                this.banner.LoadAd(request);
            }
        }

        private void BannerOnAdLeavingApplication(object sender, EventArgs args)
        {
            AmplitudeManager.LogOnInteractWithBanner();
        }
    }
}
