using System;
using Assets.Scripts.Utils;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.Ads
{
    public partial class AdManager : MonoBehaviour
    {
        private const string AppId = "ca-app-pub-9853268330632359~3927768692";
        private const string AdStorageKey = "Ads";
        public PurchaseManager PurchaseManager;
        public bool EnableAds { get; private set; } = true;


        private bool AdsNotSet = true;

        void Start()
        {
            MobileAds.Initialize(initStatus => { });

            RequestInterstitial();
            RequestRewarded();
        }

        void Update()
        {
            if (PurchaseManager.IsInitialized && AdsNotSet)
            {
                AdsNotSet = true;
                LocalStorage.SetBool(AdStorageKey,!PurchaseManager.HasBoughtNoAds());
                EnableAds = LocalStorage.GetBool(AdStorageKey);
            }
        }


        public void DisableAds()
        {
             EnableAds = false;
             LocalStorage.SetBool(AdStorageKey,false);
        }
    }
}
