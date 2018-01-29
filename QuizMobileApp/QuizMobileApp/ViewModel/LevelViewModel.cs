using QuizMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace QuizMobileApp.ViewModel
{
    public class LevelViewModel
    {
        
        public List<LevelModel> Levels { get; set; }

        public JokersModel Jokers { get; set; }

        public LevelViewModel(List<LevelModel> model, JokersModel jokers) {
            Levels = model;
            Jokers = jokers;
        }
    }
}
