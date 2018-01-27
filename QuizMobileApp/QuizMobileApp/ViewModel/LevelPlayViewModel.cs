using QuizMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.ViewModel
{
    public class LevelPlayViewModel
    {

        public LevelModel Level;
        public LevelPlayViewModel(LevelModel lvl)
        {
            Level = lvl;
        }

    }
}
