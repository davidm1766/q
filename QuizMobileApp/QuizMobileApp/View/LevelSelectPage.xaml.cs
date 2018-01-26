using QuizMobileApp.Model;
using QuizMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizMobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LevelSelectPage : ContentPage
	{
        public LevelViewModel vm;

		public LevelSelectPage ()
		{
			InitializeComponent ();
            vm = new LevelViewModel();
            listLevels.ItemsSource = vm.Levels;
            listLevels.ItemClickCommand = ItemClickCommand;
        }


        public ICommand ItemClickCommand
        {
            get
            {
                return new Command((item) =>
                {
                    LevelModel lvl = item as LevelModel;
                    
                   // DisplayAlert("title", lvl.LevelName, "cancel");
                });
            }
        }


    }
}