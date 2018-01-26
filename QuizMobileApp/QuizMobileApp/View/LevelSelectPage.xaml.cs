using QuizMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizMobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LevelSelectPage : ContentPage
	{
        LevelViewModel vm;
		public LevelSelectPage ()
		{
			InitializeComponent ();
            vm = new LevelViewModel();
            //listLevels.ItemTapped += ListLevels_ItemTapped;
            //listLevels.ItemSelected += ListLevels_ItemSelected;
            listLevels.ItemsSource = vm.Levels;
		}
        

        private void ListLevels_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            int a = 3;
        }

        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");
            //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }


        private void ToolBarItem_Navigate(object sender, EventArgs e)
        {
        }
    }
}