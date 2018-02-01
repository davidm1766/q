using Xamarin.Forms;
using Ads.Droid;
using Android.Gms.Ads;

[assembly: Dependency(typeof(AdInteresticial_Droid))]
namespace Ads.Droid
{
    public class AdInteresticial_Droid : AdListener, IAdIntersticial
    {
        private IIntersticialNotify _adsNotify;
        private InterstitialAd interstitialAd;
        
        public void Init(IIntersticialNotify adsNotify)
        {
            _adsNotify = adsNotify;
            interstitialAd = new InterstitialAd(Android.App.Application.Context);
            interstitialAd.AdUnitId = "ca-app-pub-9312615750092757/7966921878";
            interstitialAd.RewardedVideoAdFailedToLoad += InterstitialAd_RewardedVideoAdFailedToLoad;
            interstitialAd.RewardedVideoAdLoaded += InterstitialAd_RewardedVideoAdLoaded;
            interstitialAd.RewardedVideoAdClosed += InterstitialAd_RewardedVideoAdClosed;
            interstitialAd.RewardedVideoAdLeftApplication += InterstitialAd_RewardedVideoAdLeftApplication;
            interstitialAd.Rewarded += InterstitialAd_Rewarded;
        }

        private void InterstitialAd_Rewarded(object sender, Android.Gms.Ads.Reward.RewardedEventArgs e)
        {
            _adsNotify.Rewarded();   
        }

        private void InterstitialAd_RewardedVideoAdLeftApplication(object sender, System.EventArgs e)
        {
            _adsNotify.RewardedVideoAdLeftApplication();
        }

        private void InterstitialAd_RewardedVideoAdClosed(object sender, System.EventArgs e)
        {
            _adsNotify.RewardedVideoAdClosed();
        }

        private void InterstitialAd_RewardedVideoAdLoaded(object sender, System.EventArgs e)
        {
            _adsNotify.RewardedVideoAdLoaded();
        }

        private void InterstitialAd_RewardedVideoAdFailedToLoad(object sender, Android.Gms.Ads.Reward.RewardedVideoAdFailedToLoadEventArgs e)
        {
            _adsNotify.RewardedVideoAdFailedToLoad(e.ErrorCode);
        }



        public void LoadAd()
        {
            if (!interstitialAd.IsLoaded)
            {
                var requestbuilder = new AdRequest.Builder();
                requestbuilder.AddTestDevice("E8AAF6D1FAACD33793ACBCFC167B405F");
                requestbuilder.AddTestDevice(AdRequest.DeviceIdEmulator);
                interstitialAd.LoadAd(requestbuilder.Build());
            }
            
        }

        public void ShowAd()
        {
            if (interstitialAd.IsLoaded)
                interstitialAd.Show();
        }



    }
}