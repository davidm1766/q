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
        public IRepository questionRepository;

		public MainPage(IRepository repo)
		{
            questionRepository = repo;
			InitializeComponent();
		}

        private void Play_Quiz_Button_Clicked(object sender, EventArgs e)
        {
            var levels = questionRepository.GetAllLevels();
            var lvlView = new LevelViewModel(levels);
            Navigation.PushAsync(new LevelSelectPage(lvlView));
        }


    }
}
