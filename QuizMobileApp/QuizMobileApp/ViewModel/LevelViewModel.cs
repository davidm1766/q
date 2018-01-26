using QuizMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.ViewModel
{
    public class LevelViewModel
    {
        public List<LevelModel> Levels { get; set; }

        public LevelViewModel() {
            Levels = new LevelModel().GetAllLevels();
        }
    }
}
