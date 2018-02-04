using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.Model
{
    public class QuestionModel
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public int LevelId { get; set; }
        public bool IsAnswered { get; set; }
        public List<OptionInQuestionModel> Options { get; set; }

        public QuestionModel(List<OptionInQuestionModel> options)
        {
            Options = options;
        }
        
    }
}
