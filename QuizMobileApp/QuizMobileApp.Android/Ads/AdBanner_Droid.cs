using System;
using Ads;
using Android.Gms.Ads;
using Ads.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBanner_Droid))]
namespace Ads.Droid
{
    public class AdBanner_Droid : ViewRenderer {

        //public AdBanner_Droid(Context context) : base(context) {

        //}

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