using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.Ads
{
    public partial class AdManager : MonoBehaviour
    {
        private RewardedAd rewarded;

        public void RequestRewarded()
        {


        #if DEBUG
            string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // <--TEST  
        #else
            string adUnitId = "ca-app-pub-9853268330632359/5404054781";
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
        #if DEBUG
                .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
        #endif
                .Build();

            rewarded.LoadAd(request);

        }

       

        public void RunRewardedAd(EventHandler<Reward> onUserEarnedReward)
        {

#if UNITY_EDITOR
            onUserEarnedReward.Invoke(null,null);
        #else
            Debug.Log("REWARDED AD PRE");

            Debug.Log("REWARDED AD ENABLED");

            rewarded.OnUserEarnedReward += onUserEarnedReward;

            if (this.rewarded.IsLoaded())
            {
                Debug.Log("REWARDED AD SHOW");

                this.rewarded.Show();
            }
        #endif

        }

        private void RewardedHandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }
        private void RewardedHandleOnUserEarnedReward(object sender, Reward e)
        {
            AmplitudeManager.LogOnWatchRewardSucceeded();
            MonoBehaviour.print("Reward earned");
        }

        private void RewardedHandleOnAdOpened(object sender, EventArgs args)
        {
            AmplitudeManager.LogOnWatchReward();
            MonoBehaviour.print("HandleAdOpened event received");
        }

        private void RewardedHandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");
            RequestRewarded();

        }

        

    }
}
