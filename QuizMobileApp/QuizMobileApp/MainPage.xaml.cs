using QuizMobileApp.Repository;
using QuizMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
            BindingContext = this;
            this.IsBusy = false;

        }

        private void Play_Quiz_Button_Clicked(object sender, EventArgs e)
        {
            
            RunInThread();
        }

        private void SetEnableAllControl(bool enable) {
            BtnPlay.IsVisible = enable;
            BtnSetting.IsVisible = enable;
            BtnUpdate.IsVisible = enable;
        }

        private void RunInThread() {
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

                var levels = repository.GetAllLevels();
                b.ReportProgress(33);
                var jokers = repository.GetAllJokers();
                b.ReportProgress(66);
                lvlView = new LevelViewModel(levels, jokers, repository);
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
                Navigation.PushAsync(new LevelSelectPage(lvlView));
            });

            bw.RunWorkerAsync();
        }

        protected override void OnAppearing()
        {
            SetEnableAllControl(true);
        }

    }
}
