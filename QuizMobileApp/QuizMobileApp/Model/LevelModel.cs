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

        public bool IsLocked { get; set; }

        public List<QuestionModel> Questions { get; set; }


        public LevelModel(List<QuestionModel> questionModel) {
            Questions = questionModel;
        }

        public Color TxtColor {
            get {
                if (IsLocked)
                {
                    return Color.Gray;
                }
                else {
                    return Color.Black;
                }
            }
        }
        public Color LevelColor {
            get {
                if (!IsLocked) {
                    return Color.Green;
                }
                return (AllQuestionsCount - CorrectAnswersCount <= 1) ? Color.Yellow : Color.FromHex("ff8080");// Color.OrangeRed;
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
