using Ads;
using Plugin.Connectivity;
using QuizMobileApp.ViewModel;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizMobileApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LevelFailed : ContentPage, IAdsNotifty, IIntersticialNotify
	{
        public LevelFailedViewModel LevelFailedViewModel { get; set; }
        private IAdRewarded adRewardedVideo;
        private IAdIntersticial adIntersticial;

		public LevelFailed ()
		{
			InitializeComponent ();
            adRewardedVideo = DependencyService.Get<IAdRewarded>();
            adRewardedVideo.Init(this);

            adIntersticial = DependencyService.Get<IAdIntersticial>();
            adIntersticial.Init(this);
        }
        public LevelFailed(LevelFailedViewModel lfvm)
        {
            InitializeComponent();
            LevelFailedViewModel = lfvm;
            adRewardedVideo = DependencyService.Get<IAdRewarded>();
            adRewardedVideo.Init(this);

            adIntersticial = DependencyService.Get<IAdIntersticial>();
            adIntersticial.Init(this);

        }

        public void ShowCorrectClicked(object sender, EventArgs e) {
            if (CrossConnectivity.Current.IsConnected)
            {
                adRewardedVideo.LoadAd();
                //adIntersticial.LoadAd();
            }
            else
            {
                ShowErrorMessage("Nie je pripojenie na internet!");
            }
         }

        public void OnRewarded()
        {
               
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            switch (errorCode) {
                //ERROR_CODE_INTERNAL_ERROR => Something happened internally; for instance, an invalid response was received from the ad server.
                case 0: ShowErrorMessage("Internal error"); break;
                //ERROR_CODE_INVALID_REQUEST => The ad request was invalid; for instance, the ad unit ID was incorrect.
                case 1: ShowErrorMessage("Neplatné ID"); break;
                //ERROR_CODE_NETWORK_ERROR => The ad request was unsuccessful due to network connectivity.
                case 2: ShowErrorMessage("Nie je pripojenie na internet!"); break;
                //ERROR_CODE_NO_FILL => The ad request was successful, but no ad was returned due to lack of ad inventory. Nie je ziadna reklama od googlu uz...
                case 3: adIntersticial.LoadAd(); break;
            }
            
        }

        private void ShowErrorMessage(string message)
        {
            DisplayAlert("Chyba", message, "Späť");
        }

        public void OnRewardedVideoAdOpened()
        {
    
        }

        public void OnRewardedVideoAdLoaded()
        {
            adRewardedVideo.ShowAd();
        }

        public void OnRewardedVideoAdLeftApplication()
        {
           
        }

        public void OnRewardedVideoAdClosed()
        {
         
        }
        #region Interestitial
        public void RewardedVideoAdFailedToLoad(int error)
        {
            switch (error)
            {
                //ERROR_CODE_INTERNAL_ERROR => Something happened internally; for instance, an invalid response was received from the ad server.
                case 0: ShowErrorMessage("Internal error"); break;
                //ERROR_CODE_INVALID_REQUEST => The ad request was invalid; for instance, the ad unit ID was incorrect.
                case 1: ShowErrorMessage("Neplatné ID"); break;
                //ERROR_CODE_NETWORK_ERROR => The ad request was unsuccessful due to network connectivity.
                case 2: ShowErrorMessage("Nie je pripojenie na internet!"); break;
                //ERROR_CODE_NO_FILL => The ad request was successful, but no ad was returned due to lack of ad inventory. Nie je ziadna reklama od googlu uz...
                case 3: ShowErrorMessage("V sučastnosti nie sú dostupné žiadne reklamy"); break;
            }
        }

        public void RewardedVideoAdLoaded()
        {
            adIntersticial.ShowAd();
        }

        public void RewardedVideoAdLeftApplication()
        {
            
        }

        public void RewardedVideoAdClosed()
        {
            
        }

        public void Rewarded()
        {
            //rewarded
        }
        #endregion
    }
}