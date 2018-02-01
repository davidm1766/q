using System;
using Ads;
using Ads.Droid;
using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdInteresticialView_Droid))]
namespace Ads.Droid
{

    public class AdInteresticialView_Droid : ViewRenderer, IRewardedVideoAdListener
    {        
        
        private IAdsNotifty _iadinter;
        public AdInteresticialView_Droid() : base(Android.App.Application.Context)
        {
        }

        public AdInteresticialView_Droid(IAdsNotifty inter) : base(Android.App.Application.Context)
        {
            _iadinter = inter;
        }

        public void OnRewarded(IRewardItem reward)
        {
            _iadinter.OnRewarded();
        }

        public void OnRewardedVideoAdClosed()
        {
            _iadinter.OnRewardedVideoAdClosed();
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            _iadinter.OnRewardedVideoAdFailedToLoad(errorCode);
        }

        public void OnRewardedVideoAdLeftApplication()
        {
            _iadinter.OnRewardedVideoAdLeftApplication();
        }

        public void OnRewardedVideoAdLoaded()
        {
            _iadinter.OnRewardedVideoAdLoaded();
        }

        public void OnRewardedVideoAdOpened()
        {
            _iadinter.OnRewardedVideoAdOpened();
        }

        public void OnRewardedVideoStarted()
        {

        }

        public void Dispose()
        {

        }

       
       
    }
}