using QuizMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.ViewModel
{
    public class LevelFailedViewModel
    {
        public LevelPlayViewModel LevelPlayViewModel { get; set; }
        public List<OptionInQuestionModel> OptionsInQuestion { get; set; }


        public LevelFailedViewModel(LevelPlayViewModel levelPlayViewModel, List<OptionInQuestionModel> options)
        {
            this.OptionsInQuestion = options;
            this.LevelPlayViewModel = levelPlayViewModel;
        }
    }
}
