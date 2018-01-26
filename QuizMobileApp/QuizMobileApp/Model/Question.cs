using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.Model
{
    public class Question
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public List<OptionInQuestionModel> Options { get; set; }

        public Question(int IdQuestion)
        {
            this.IdQuestion = IdQuestion;
            Options = new List<OptionInQuestionModel>();
            this.LoadOptions();
        }

        private void LoadOptions()
        {
            
        }
    }
}
