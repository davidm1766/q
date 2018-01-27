using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace QuizMobileApp.Model
{
    public class LevelModel
    {
        public int IdLevel { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int AllQuestionsCount { get; set; }

        public List<QuestionModel> Questions { get; set; }


        public LevelModel(List<QuestionModel> questionModel) {
            Questions = questionModel;
        }

        public Color LevelColor {
            get {
                return (AllQuestionsCount - CorrectAnswersCount <= 1) ? Color.Yellow : Color.Red;
            }
        }

        public string LevelName {
            get {
                return $"Level {IdLevel} ";
            }
        }

        public string LevelInfo
        {
            get {
                return $"Počet otázok {CorrectAnswersCount}/{AllQuestionsCount} ";
            }
        }


    }
}
