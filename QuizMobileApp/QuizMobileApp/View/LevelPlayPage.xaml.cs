﻿using QuizMobileApp.ViewModel;
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
	public partial class LevelPlayPage : ContentPage
	{

        public LevelPlayViewModel levelViewModel;
		public LevelPlayPage (LevelPlayViewModel lvlVM)
		{
            lvlVM = levelViewModel;
			InitializeComponent ();
		}
	}
}