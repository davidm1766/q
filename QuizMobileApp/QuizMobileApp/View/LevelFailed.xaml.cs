using Ads;
using QuizMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizMobileApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LevelFailed : ContentPage
	{
        public LevelFailedViewModel LevelFailedViewModel { get; set; }

		public LevelFailed ()
		{
			InitializeComponent ();
		}
        public LevelFailed(LevelFailedViewModel lfvm)
        {
            InitializeComponent();
            LevelFailedViewModel = lfvm;
        }

        public void ShowCorrectClicked(object sender, EventArgs e) {
            IAdInterstitial adInterstitial = DependencyService.Get<IAdInterstitial>();
            adInterstitial.ShowAd();
        }


    }
}