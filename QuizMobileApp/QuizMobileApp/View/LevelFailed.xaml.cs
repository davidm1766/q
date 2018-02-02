using Ads;
using Plugin.Connectivity;
using QuizMobileApp.ViewModel;
using System;
using System.Linq;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizMobileApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LevelFailed : ContentPage, IAdsNotifty, IIntersticialNotify
	{
        public LevelFailedViewModel LevelFailedVM { get; set; }
        private IAdRewarded adRewardedVideo;
        private IAdIntersticial adIntersticial;

		public LevelFailed ()
		{
			InitializeComponent ();
            //adRewardedVideo = DependencyService.Get<IAdRewarded>();
            //adRewardedVideo.Init(this);

            //adIntersticial = DependencyService.Get<IAdIntersticial>();
            //adIntersticial.Init(this);
        }
        public LevelFailed(LevelFailedViewModel lfvm)
        {
            InitializeComponent();
            LevelFailedVM = lfvm;
            adRewardedVideo = DependencyService.Get<IAdRewarded>();
            adRewardedVideo.Init(this);

            adIntersticial = DependencyService.Get<IAdIntersticial>();
            adIntersticial.Init(this);
            questionLbl.Text = LevelFailedVM.LevelPlayViewModel.GetActualQuestion().QuestionText;
        }

        public void ShowCorrectClicked(object sender, EventArgs e) {
            adRewardedVideo.LoadAd();
            return;
            if (CrossConnectivity.Current.IsConnected)
            {
                adRewardedVideo.LoadAd();
            }
            else
            {
                ShowErrorMessage("Nie je pripojenie na internet!");
            }
         }

        public void OnRewarded()
        {
            RewardOK();
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            switch (errorCode) {
                //ERROR_CODE_INTERNAL_ERROR => Something happened internally; for instance, an invalid response was received from the ad server.
                case 0: adIntersticial.LoadAd(); break; //ShowErrorMessage("Internal error"); break;
                //ERROR_CODE_INVALID_REQUEST => The ad request was invalid; for instance, the ad unit ID was incorrect.
                case 1: adIntersticial.LoadAd(); break; //ShowErrorMessage("Neplatné ID"); break;
                //ERROR_CODE_NETWORK_ERROR => The ad request was unsuccessful due to network connectivity.
                case 2: RewardOK();break;// ShowErrorMessage("Nie je pripojenie na internet!"); break;
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
            RewardFail();
        }

        public void OnRewardedVideoAdClosed()
        {
            RewardFail(); 
        }

        #region Interestitial
        public void RewardedVideoAdFailedToLoad(int error)
        {
            switch (error)
            {
                //ERROR_CODE_INTERNAL_ERROR => Something happened internally; for instance, an invalid response was received from the ad server.
                case 0: RewardOK(); break;
                //ERROR_CODE_INVALID_REQUEST => The ad request was invalid; for instance, the ad unit ID was incorrect.
                case 1: RewardOK(); break;
                //ERROR_CODE_NETWORK_ERROR => The ad request was unsuccessful due to network connectivity.
                case 2: ShowErrorMessage("Nie je pripojenie na internet!"); break;
                //ERROR_CODE_NO_FILL => The ad request was successful, but no ad was returned due to lack of ad inventory. Nie je ziadna reklama od googlu uz...
                case 3: RewardOK(); break;
            }
        }

        public void RewardedVideoAdLoaded()
        {
            adIntersticial.ShowAd();
        }

        public void RewardedVideoAdLeftApplication()
        {
            RewardFail();
        }

        public void RewardedVideoAdClosed()
        {
            RewardOK();
        }

        public void Rewarded()
        {
            RewardOK();
        }
        #endregion

        public void RewardOK() {
            var correct = LevelFailedVM.OptionsInQuestion.Where(x=>x.IsCorrect);
            string text = "";
            foreach (var c in correct) {
                text += c.Text + " \n";
            }
            correctAnswer.Text = text;
            successfullyContinueBtn.Clicked -= ShowCorrectClicked;
            successfullyContinueBtn.Clicked += ContinueClicked;
            successfullyContinueBtn.Text = "Pokračovať v hre";
        }

        public void RewardFail() {

        }

        public void TryAgainClicked(object sender, EventArgs e)
        {
            //Neuspesny back
            LevelFailedVM.LevelPlayViewModel.CanContinue = false;
            LevelFailedVM.LevelPlayViewModel.IsReturnedFromModal = true;
            Navigation.PopAsync();
        }

        public void ContinueClicked(object sender, EventArgs e)
        {
            //je tu uspesny back
            LevelFailedVM.LevelPlayViewModel.CanContinue = true;
            LevelFailedVM.LevelPlayViewModel.IsReturnedFromModal = true;
            Navigation.PopAsync();
        }
    }
}