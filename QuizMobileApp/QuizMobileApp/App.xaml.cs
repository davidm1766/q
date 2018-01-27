using QuizMobileApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace QuizMobileApp
{
	public partial class App : Application
	{
		public App (IRepository repo)
		{
			InitializeComponent();

            //MainPage = new QuizMobileApp.MainPage();
            MainPage = new NavigationPage(new MainPage(repo));
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
