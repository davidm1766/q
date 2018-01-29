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
using Microcharts.Forms;
using Microcharts;
using SkiaSharp;

namespace QuizMobileApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelPlayPage : ContentPage
    {

        public LevelPlayViewModel LevelViewModel;
        public JokersModel Jokers {get;set;}
        private Random _rnd;
        private List<OptionInQuestionModel> actualOptions;


        //because preview
        public LevelPlayPage() {
            InitializeComponent();
            _rnd = new Random();
        }

		public LevelPlayPage (LevelPlayViewModel lvlVM,JokersModel jokers)
		{
			InitializeComponent ();
            Jokers = jokers;
            _rnd = new Random();
            LevelViewModel = lvlVM;
            LblQuestion.Text = LevelViewModel.GetActualQuestion().QuestionText;
            actualOptions = LevelViewModel.GetActualQuestion().Options.OrderBy((item) => _rnd.Next()).ToList();
            listOptions.ItemsSource = actualOptions;
            listOptions.ItemClickCommand = ItemClickCommand;

            
            var fiftyTapRecognizer = new TapGestureRecognizer
            {
                Command = new Command(OnFiftyFifty),
                NumberOfTapsRequired = 1
            };
            fiftyfiftyImg.GestureRecognizers.Add(fiftyTapRecognizer);

            var peopleTapRecognizer = new TapGestureRecognizer
            {
                Command = new Command(OnPeopleTap),
                NumberOfTapsRequired = 1
            };
            peopleImg.GestureRecognizers.Add(peopleTapRecognizer);
        }

        private void OnPeopleTap(object obj)
        {
            var opti = actualOptions;
            var count = opti.Count;
            List<int> randomVoting = new List<int>();
            List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
            for (int i = 0; i < count; i++)
            {
                randomVoting.Add(_rnd.Next(100));                
            }
            randomVoting = randomVoting.OrderByDescending(x=>x).ToList();
            
            //jokerDisplay.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //for (int i = 0; i < count; i++)
            //{
            //    jokerDisplay.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //}
            for (int i = 0; i < count; i++)
            {
                int cnt = -1;
                if (opti[i].IsCorrect)
                {
                    cnt = randomVoting[0];
                    randomVoting.RemoveAt(0);
                }
                else
                {                    
                    if (randomVoting.Count == 1)
                    {
                        cnt = randomVoting[0];
                        randomVoting.RemoveAt(0);
                    }
                    else {
                        cnt = randomVoting[1];
                        randomVoting.RemoveAt(1);
                    }
                }


                //jokerDisplay.Children.Add(new Label() { Text = $"{i+1}: {cnt}" },0,i);
                SKColor col;
                switch (i % 6) {
                    case 0: col = SKColor.Parse("#8590d5");break;
                    case 1: col = SKColor.Parse("#266489");break;
                    case 2: col = SKColor.Parse("#68B9C0");break;

                    case 3: col = SKColor.Parse("#90D585"); break;
                    case 4: col = SKColor.Parse("#d5ca85"); break;
                    case 5: col = SKColor.Parse("#99ccdd"); break;
                    default: col = SKColor.Parse("#99ccdd"); break;
                }
                 
                
                
                entries.Add(new Microcharts.Entry(cnt) { Label = $"{i + 1}",Color=col });
            }
            ChartView ch = new ChartView() { HeightRequest = 150 };
            ch.Chart = new BarChart() { Entries = entries,MaxValue=120 };
            jokerDisplay.Children.Add(ch);
        }

        public void OnFiftyFifty() {
            DisplayAlert("tuk", "50/50", "Cancel");
            Jokers.Joker50on50--;
            if (Jokers.Joker50on50 <= 0) {
                fiftyfiftyImg.IsVisible = false;
            }
            var count = actualOptions.Count/2 - 1;
            var list = actualOptions;
            var srcList = new List<OptionInQuestionModel>();
            foreach (var opt in list) {
                if (opt.IsCorrect)
                {
                    srcList.Add(opt);
                }
                else
                {
                    if (count > 0)
                    {
                        srcList.Add(opt);
                        count--;
                    }
                    
                }
                
            }
            actualOptions = srcList;
            listOptions.ItemsSource = srcList;
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
            jokerDisplay.Children.Clear();
            var quest = LevelViewModel.GetNextQuestion();
            if (quest == null)
            {
                DisplayAlert("NOVY LEVEL", "NOVY LEVEL", "Cancel");
            }
            else {

                LblQuestion.Text = LevelViewModel.GetActualQuestion().QuestionText;
                actualOptions = LevelViewModel.GetActualQuestion().Options.OrderBy((item) => _rnd.Next()).ToList();
                listOptions.ItemsSource = actualOptions;
            }
        }
    }
}