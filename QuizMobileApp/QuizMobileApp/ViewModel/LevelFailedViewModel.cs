using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.ViewModel
{
    public class LevelFailedViewModel
    {
        public LevelPlayViewModel LevelPlayViewModel { get; set; }

        public LevelFailedViewModel(LevelPlayViewModel levelPlayViewModel)
        {
            this.LevelPlayViewModel = levelPlayViewModel;
        }
    }
}
