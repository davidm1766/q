using Ads;
using QuizMobileApp.ViewModel;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizMobileApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LevelFailed : ContentPage, IAdsNotifty
	{
        public LevelFailedViewModel LevelFailedViewModel { get; set; }
        private IAdInterstitial adInterstitial;

		public LevelFailed ()
		{
			InitializeComponent ();
            adInterstitial = DependencyService.Get<IAdInterstitial>();
            adInterstitial.Init(this);
        }
        public LevelFailed(LevelFailedViewModel lfvm)
        {
            InitializeComponent();
            LevelFailedViewModel = lfvm;
            adInterstitial = DependencyService.Get<IAdInterstitial>();
            adInterstitial.Init(this);

            
        }

        public void ShowCorrectClicked(object sender, EventArgs e) {
            adInterstitial.LoadAd();
         }

        public void OnRewarded()
        {
            
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            switch (errorCode) {
                //ERROR_CODE_INTERNAL_ERROR => Something happened internally; for instance, an invalid response was received from the ad server.
                case 0: ShowErrorMessage("Internall error"); break;
                //ERROR_CODE_INVALID_REQUEST => The ad request was invalid; for instance, the ad unit ID was incorrect.
                case 1: ShowErrorMessage("Neplatne ID"); break;
                //ERROR_CODE_NETWORK_ERROR => The ad request was unsuccessful due to network connectivity.
                case 2: ShowErrorMessage("Nie je pripojenie na internet!"); break;
                //ERROR_CODE_NO_FILL => The ad request was successful, but no ad was returned due to lack of ad inventory. Nie je ziadna reklama od googlu uz...
                case 3: ShowErrorMessage("V sučastnosti nie su dostupné žiadne reklamy"); break;
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
            adInterstitial.ShowAd();
        }

        public void OnRewardedVideoAdLeftApplication()
        {
           
        }

        public void OnRewardedVideoAdClosed()
        {
         
        }
    }
}