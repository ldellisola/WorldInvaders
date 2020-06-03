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
        private InterstitialAd interstitial;

        public void RequestInterstitial()
        {
            if (EnableAds)
            {
#if UNITY_ANDROID
                string adUnitId =
                    "ca-app-pub-9853268330632359/1494296604"; //"ca-app-pub-3940256099942544/1033173712"; // <--TEST 
#else
        string adUnitId = "unexpected_platform";
#endif

                // Initialize an InterstitialAd.
                this.interstitial = new InterstitialAd(adUnitId);



                // Called when an ad request has successfully loaded.
                this.interstitial.OnAdLoaded += InterstitialHandleOnAdLoaded;
                // Called when an ad request failed to load.
                this.interstitial.OnAdFailedToLoad += InterstitialHandleOnAdFailedToLoad;
                // Called when an ad is shown.
                this.interstitial.OnAdOpening += InterstitialHandleOnAdOpened;
                // Called when the ad is closed.
                this.interstitial.OnAdClosed += InterstitialHandleOnAdClosed;
                // Called when the ad click caused the user to leave the application.
                this.interstitial.OnAdLeavingApplication += InterstitialHandleOnAdLeavingApplication;

                AdRequest request = new AdRequest.Builder()
                    // .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
                    .Build();

                interstitial.LoadAd(request);
            }

        }

        public void RunIntertitialAd(EventHandler<EventArgs> onAdCloses)
        {
            Debug.Log("INTERTITAL AD PRE");

            if (this.EnableAds)
            {
                Debug.Log("AD ENABLED");
            }

            if (this.EnableAds && this.interstitial.IsLoaded())
            {
                Debug.Log("INTERTITAL AD RUN");

                this.interstitial.OnAdClosed += onAdCloses;
                this.interstitial.Show();
            }


        }

        private void InterstitialHandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        private void InterstitialHandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                                + args.Message);
        }

        private void InterstitialHandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        private void InterstitialHandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
            RequestInterstitial();
        }

        private void InterstitialHandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }
    }
}
