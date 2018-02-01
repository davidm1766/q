using System;
using Ads;
using Android.Gms.Ads;
using Ads.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Gms.Ads.Reward;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBanner_Droid))]
namespace Ads.Droid
{
    public class AdBanner_Droid : ViewRenderer, IRewardedVideoAdListener
    {
        private IAdsNotifty _iadinter;
        public AdBanner_Droid() : base(Android.App.Application.Context)
        {
        }

        public AdBanner_Droid(IAdsNotifty inter) : base(Android.App.Application.Context) {
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

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var adView = new AdView(Context);

                switch ((Element as AdBanner).Size)
                {
                    case AdBanner.Sizes.Standardbanner:
                        adView.AdSize = AdSize.Banner;
                        break;
                    case AdBanner.Sizes.LargeBanner:
                        adView.AdSize = AdSize.LargeBanner;
                        break;
                    case AdBanner.Sizes.MediumRectangle:
                        adView.AdSize = AdSize.MediumRectangle;
                        break;
                    case AdBanner.Sizes.FullBanner:
                        adView.AdSize = AdSize.FullBanner;
                        break;
                    case AdBanner.Sizes.Leaderboard:
                        adView.AdSize = AdSize.Leaderboard;
                        break;
                    case AdBanner.Sizes.SmartBannerPortrait:
                        adView.AdSize = AdSize.SmartBanner;
                        break;
                    default:
                        adView.AdSize = AdSize.Banner;
                        break;
                }

                // TODO: change this id to your admob id
                adView.AdUnitId = "ca-app-pub-9312615750092757~3504098531";//"ca-app-pub-9312615750092757/7966921878";//"ca-app-pub-9312615750092757~3504098531";

                var requestbuilder = new AdRequest.Builder();
                requestbuilder.AddTestDevice("E8AAF6D1FAACD33793ACBCFC167B405F");
                requestbuilder.AddTestDevice(AdRequest.DeviceIdEmulator);
                
                adView.LoadAd(requestbuilder.Build());
                
                SetNativeControl(adView);
            }
        }
    }
}