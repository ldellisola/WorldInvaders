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
        private RewardedAd rewarded;

        public void RequestRewarded()
        {

            if (EnableAds)
            {
#if UNITY_ANDROID
                string adUnitId =
                    "ca-app-pub-9853268330632359/5404054781"; //"ca-app-pub-3940256099942544/5224354917"; // <--TEST 
#else
        string adUnitId = "unexpected_platform";
#endif

                // Initialize an InterstitialAd.
                this.rewarded = new RewardedAd(adUnitId);

                // Called when an ad request has successfully loaded.
                this.rewarded.OnAdLoaded += RewardedHandleOnAdLoaded;
                // Called when an ad request failed to load.
                rewarded.OnUserEarnedReward += RewardedHandleOnUserEarnedReward;
                // Called when an ad is shown.
                this.rewarded.OnAdOpening += RewardedHandleOnAdOpened;
                // Called when the ad is closed.
                this.rewarded.OnAdClosed += RewardedHandleOnAdClosed;
                // Called when the ad click caused the user to leave the application.

                AdRequest request = new AdRequest.Builder()
                    // .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
                    .Build();

                rewarded.LoadAd(request);
            }

        }

        private void RewardedHandleOnUserEarnedReward(object sender, Reward e)
        {
            MonoBehaviour.print("Reward earned");
        }

        public void RunRewardedAd( EventHandler<Reward> onUserEarnedReward)
        {
            Debug.Log("REWARDED AD PRE");
            if (EnableAds)
            {
                Debug.Log("REWARDED AD ENABLED");

                rewarded.OnUserEarnedReward += onUserEarnedReward;

                if (this.rewarded.IsLoaded())
                {
                    Debug.Log("REWARDED AD SHOW");

                    this.rewarded.Show();
                }
            }
        }

        private void RewardedHandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }


        private void RewardedHandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        private void RewardedHandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
            RequestRewarded();
        }

        private void RewardedHandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }

    }
}
