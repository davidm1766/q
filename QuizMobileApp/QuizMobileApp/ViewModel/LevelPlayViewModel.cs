using QuizMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.ViewModel
{
    public class LevelPlayViewModel
    {

        public LevelModel Level;
        public bool CanContinue { get; set; }
        public bool IsReturnedFromModal { get; set; }
        private int _actualQuestion;

        public LevelPlayViewModel(LevelModel lvl)
        {
            Level = lvl;
            _actualQuestion = 0;
            CanContinue = false;
            IsReturnedFromModal = false;
        }

        public QuestionModel GetActualQuestion()
        {

            return Level.Questions[_actualQuestion];
        }

        public QuestionModel GetNextQuestion() {
            //no more questions
            if (_actualQuestion >= Level.Questions.Count-1) {
                return null;
            }
            return Level.Questions[++_actualQuestion];
        }

    }
}
