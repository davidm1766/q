using Android.Gms.Ads;
using Ads.Droid;
using Xamarin.Forms;
using Android.Gms.Ads.Reward;
using System;
using Android.Net;

[assembly: Dependency(typeof(AdRewarded_Droid))]
namespace Ads.Droid
{
    public class AdRewarded_Droid : IAdRewarded
    {
        //InterstitialAd interstitialAd;
        private IRewardedVideoAd rewardedAd;
        private IAdsNotifty _adsNotify;

        public void Init(IAdsNotifty adsNotify)
        {
            _adsNotify = adsNotify;
            rewardedAd = MobileAds.GetRewardedVideoAdInstance(Android.App.Application.Context);
            rewardedAd.RewardedVideoAdListener = new AdRewardedView_Droid(_adsNotify);
        }

        public AdRewarded_Droid()
        {
            //interstitialAd = new InterstitialAd(Android.App.Application.Context);
            //interstitialAd.Rewarded += InterstitialAd_Rewarded;
            //interstitialAd.RewardedVideoAdFailedToLoad += InterstitialAd_RewardedVideoAdFailedToLoad;
            //interstitialAd.RewardedVideoStarted += InterstitialAd_RewardedVideoStarted;
            //interstitialAd.RewardedVideoAdClosed += InterstitialAd_RewardedVideoAdClosed;
            //interstitialAd.RewardedVideoAdOpened += InterstitialAd_RewardedVideoAdOpened;
            //// TODO: change this id to your admob id
            //interstitialAd.AdUnitId = "ca-app-pub-9312615750092757/7766653462";

        }


        public void LoadAd()
        {
            if (!rewardedAd.IsLoaded)
            {
                var requestbuilder = new AdRequest.Builder();
                requestbuilder.AddTestDevice("E8AAF6D1FAACD33793ACBCFC167B405F");
                requestbuilder.AddTestDevice(AdRequest.DeviceIdEmulator);
                rewardedAd.LoadAd("ca-app-pub-9312615750092757/7766653462", requestbuilder.Build());
            }
        }


        public void ShowAd()
        {
            if (rewardedAd.IsLoaded)
            {
                rewardedAd.Show();
            }
            //LoadAd();
        }

        
    }

    
}