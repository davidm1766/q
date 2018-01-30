using Android.Gms.Ads;
using Ads.Droid;
using Xamarin.Forms;
using Android.Gms.Ads.Reward;
using System;

[assembly: Dependency(typeof(AdInterstitial_Droid))]
namespace Ads.Droid
{
    public class AdInterstitial_Droid : IAdInterstitial
    {
        //InterstitialAd interstitialAd;
        IRewardedVideoAd rewardedAd;


         
        public AdInterstitial_Droid()
        {
            //interstitialAd = new InterstitialAd(Android.App.Application.Context);
            //interstitialAd.Rewarded += InterstitialAd_Rewarded;
            //interstitialAd.RewardedVideoAdFailedToLoad += InterstitialAd_RewardedVideoAdFailedToLoad;
            //interstitialAd.RewardedVideoStarted += InterstitialAd_RewardedVideoStarted;
            //interstitialAd.RewardedVideoAdClosed += InterstitialAd_RewardedVideoAdClosed;
            //interstitialAd.RewardedVideoAdOpened += InterstitialAd_RewardedVideoAdOpened;
            //// TODO: change this id to your admob id
            //interstitialAd.AdUnitId = "ca-app-pub-9312615750092757/7766653462";
            rewardedAd = MobileAds.GetRewardedVideoAdInstance(Android.App.Application.Context);
            rewardedAd.RewardedVideoAdListener = new AdBanner_Droid(this);            
            
            LoadAd();
        }

      
        void LoadAd()
        {
            var requestbuilder = new AdRequest.Builder();
            requestbuilder.AddTestDevice("E8AAF6D1FAACD33793ACBCFC167B405F");
            requestbuilder.AddTestDevice(AdRequest.DeviceIdEmulator);
            rewardedAd.LoadAd("ca-app-pub-9312615750092757/7766653462", requestbuilder.Build()); 
        }

        public void Rewarded(bool state) {
            var a = state;
        }

        public void ShowAd()
        {
            if (rewardedAd.IsLoaded)
            {
                rewardedAd.Show();
            }
            LoadAd();
        }

       
    }

    
}