using QuizMobileApp.Model;
using QuizMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizMobileApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LevelPlayPage : ContentPage
	{

        public LevelPlayViewModel LevelViewModel;
        private Random _rnd; 
        //because preview
        public LevelPlayPage() {
            InitializeComponent();
            _rnd = new Random();
        }

		public LevelPlayPage (LevelPlayViewModel lvlVM)
		{
			InitializeComponent ();
            _rnd = new Random();
            LevelViewModel = lvlVM;
            LblQuestion.Text = LevelViewModel.GetActualQuestion().QuestionText;
            listOptions.ItemsSource = LevelViewModel.GetActualQuestion().Options.OrderBy((item)=>_rnd.Next());
            listOptions.ItemClickCommand = ItemClickCommand;
        }

        public ICommand ItemClickCommand
        {
            get
            {
                return new Command((item) =>
                {
                    OptionInQuestionModel que= item as OptionInQuestionModel;
                    if (que.IsCorrect)
                    {
                        NextQuestion();
                    }
                    else {
                        IncorrectAnswer();
                    }
                });
            }
        }

        private void IncorrectAnswer()
        {
            DisplayAlert("Nespravna", "Zla odpoved", "Cancel");
        }

        private void NextQuestion()
        {
            var quest = LevelViewModel.GetNextQuestion();
            if (quest == null)
            {
                DisplayAlert("NOVY LEVEL", "NOVY LEVEL", "Cancel");
            }
            else {

                LblQuestion.Text = LevelViewModel.GetActualQuestion().QuestionText;
                listOptions.ItemsSource = LevelViewModel.GetActualQuestion().Options.OrderBy((item) => _rnd.Next());
            }
        }
    }
}