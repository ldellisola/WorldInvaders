using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ads
{
    public class AmplitudeManager : MonoBehaviour
    {
        private readonly string APIKey = "f98ed6d700d9443939da88edc1d9c8d2";
        private Amplitude amplitude;
        void Awake () {
            amplitude = Amplitude.Instance;
            amplitude.logging = true;
            // amplitude.trackSessionEvents(true);
            amplitude.init(APIKey);
            DontDestroyOnLoad(this.gameObject);
        }


        public static void LogOnPurchaseStarted()
        {
            const string eventName = "Purchased No Ads Started";
            Amplitude.Instance.logEvent(eventName);
        }

        public static void LogOnPurchaseFailed()
        {
            const string eventName = "Purchased No Ads Failed";
            Amplitude.Instance.logEvent(eventName);
        }

        public static void LogOnPurchaseSucceeded()
        {
            const string eventName = "Purchased No Ads Succeeded";
            Amplitude.Instance.logEvent(eventName);
        }

        
        public static void LogOnSelectedUnlimitedMode()
        {
            const string eventName = "Playing Unlimited Mode";
            Amplitude.Instance.logEvent(eventName);
        }
           
        public static void LogOnSelectedLevelsMenu()
        {
            const string eventName = "Entered Select Level Menu";
            Amplitude.Instance.logEvent(eventName);
        }

        
        public static void LogOnSelectedLevel(string levelNum)
        {
            const string eventName = "Selected Level";
            Amplitude.Instance.logEvent(eventName,new Dictionary<string, object>
            {
                {"Level", levelNum}
            });
        }

        public static void LogOnWatchReward()
        {
            const string eventName = "Started Watching Rewarded Video";
            Amplitude.Instance.logEvent(eventName);
        }

        public static void LogOnWatchRewardSucceeded()
        {
            const string eventName = "Watching Rewarded Video Succeeded";
            Amplitude.Instance.logEvent(eventName);
        }

        public static void LogOnGoBackToMenuOnGame()
        {
            const string eventName = "Return To Menu On Game Over";
            Amplitude.Instance.logEvent(eventName);
        }

        public static void LogOnInteractWithBanner()
        {
            const string eventName = "Click on Banner";
            Amplitude.Instance.logEvent(eventName);
        }

        public static void LogOnInteractWithInterstitial()
        {
            const string eventName = "Click on Intestitial";
            Amplitude.Instance.logEvent(eventName);
        }

        
        public static void LogOnReturnToMenuFromLevels()
        {
            const string eventName = "Return to Menu from Levels";
            Amplitude.Instance.logEvent(eventName);
        }
    }
}
