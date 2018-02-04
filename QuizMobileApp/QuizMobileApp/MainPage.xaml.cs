using QuizMobileApp.Repository;
using QuizMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizMobileApp
{
	public partial class MainPage : ContentPage
	{
        public IRepository repository;

		public MainPage(IRepository repo)
		{
            //ca-app-pub-9312615750092757~3504098531
            //aapk => ca-app-pub-9312615750092757~3504098531
            //reklama => ca-app-pub-9312615750092757/7766653462
            repository = repo;
			InitializeComponent();
		}

        private void Play_Quiz_Button_Clicked(object sender, EventArgs e)
        {
            var levels = repository.GetAllLevels();
            var jokers = repository.GetAllJokers();
            var lvlView = new LevelViewModel(levels,jokers,repository);
            Navigation.PushAsync(new LevelSelectPage(lvlView));
        }


    }
}
