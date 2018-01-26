﻿using System;
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

        public List<LevelModel> GetAllLevels() {
            List<LevelModel> lv = new List<LevelModel>();
            lv.Add(new LevelModel() { IdLevel = 1, AllQuestionsCount = 10, CorrectAnswersCount = 9 });
            lv.Add(new LevelModel() { IdLevel = 2, AllQuestionsCount = 10, CorrectAnswersCount = 8 });
            lv.Add(new LevelModel() { IdLevel = 3, AllQuestionsCount = 10, CorrectAnswersCount = 1 });
            lv.Add(new LevelModel() { IdLevel = 4, AllQuestionsCount = 10, CorrectAnswersCount = 0 });
            return lv;
        }
    }
}
