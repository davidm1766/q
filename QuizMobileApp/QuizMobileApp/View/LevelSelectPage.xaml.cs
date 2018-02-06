using QuizMobileApp.Model;
using QuizMobileApp.View;
using QuizMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizMobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LevelSelectPage : ContentPage
	{
        public LevelViewModel vm { get; set; }
        private bool firstLoaded;
        public LevelSelectPage() {
            InitializeComponent();
        }

		public LevelSelectPage (LevelViewModel lvlVM)
		{
			InitializeComponent ();
            IsBusy = false;
            firstLoaded = true;
            vm = lvlVM;
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
                    if (lvl.IsLocked) {
                        return;
                    }
                    var level = vm.Levels.Where(x => x.IdLevel == lvl.IdLevel).FirstOrDefault();
                    Navigation.PushAsync(new LevelPlayPage(new LevelPlayViewModel(level,vm.Repository,vm),vm.Jokers));
                });
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (!firstLoaded)
            {
                //MainGrid.IsVisible = true;
                //AIncicator.IsVisible = true;                
                LblSelectLevel.Text = "Prosím počkajte...";
                LoadDataInThread();
            }
            else {
                firstLoaded = false;
            }
        }

        private void LoadDataInThread()
        {
            IsBusy = true;
            SetEnableAllControl(false);
            LevelViewModel lvlView = null;
            BackgroundWorker bw = new BackgroundWorker();
            // this allows our worker to report progress during work
            bw.WorkerReportsProgress = true;

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(
            delegate (object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                vm.ReloadLevels();
                b.ReportProgress(100);

            });

            // what to do when progress changed (update the progress bar for example)
            bw.ProgressChanged += new ProgressChangedEventHandler(
            delegate (object o, ProgressChangedEventArgs args)
            {
                //  label1.Text = string.Format("{0}% Completed", args.ProgressPercentage);
            });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate (object o, RunWorkerCompletedEventArgs args)
            {
                IsBusy = false;
                listLevels.ItemsSource = vm.Levels;
                listLevels.ItemClickCommand = ItemClickCommand;
                LblSelectLevel.Text = "Výber levelu";
                SetEnableAllControl(true);
            });

            bw.RunWorkerAsync();
        }

        private void SetEnableAllControl(bool v)
        {
            listLevels.IsVisible = v;
        }
    }
}