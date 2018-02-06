using QuizMobileApp.Model;
using QuizMobileApp.Repository;
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
        public IRepository Repository { get; set; }
        public int IsReturnedFromLevel { get; set; }

        public LevelViewModel(List<LevelModel> model, JokersModel jokers, IRepository repository)
        {
            Repository = repository; 
            Levels = model;
            Jokers = jokers;
            IsReturnedFromLevel = -1;
        }

        public void ReloadLevels() {
            Levels = Repository.GetAllLevels();
        }
        
    }
}
