using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.Model
{
    public class OptionInQuestionModel
    {
        public int IdOption { get; set; }
        public int IdQuestion { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
    }
}
