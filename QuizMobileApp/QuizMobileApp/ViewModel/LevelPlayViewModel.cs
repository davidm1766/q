using QuizMobileApp.Model;
using QuizMobileApp.Repository;
using System.Collections.Generic;
using System.Linq;


namespace QuizMobileApp.ViewModel
{
    public class LevelPlayViewModel
    {

        public LevelModel Level;
        public bool CanContinue { get; set; }
        public bool IsReturnedFromModal { get; set; }
        public IRepository Repository { get; set; }
        public LevelViewModel LevelViewModel { get; set; }
        public int MaxLevelId { get; }
        private int _actualQuestion;


        public LevelPlayViewModel(LevelModel lvl, IRepository repository,LevelViewModel levels)
        {
            LevelViewModel = levels; 
            MaxLevelId = levels.Levels.Max(x => x.IdLevel);
            Repository = repository;
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

        public void WriteLevelDone(List<QuestionModel> questions,int idLevel,int levelsCount)
        {
            Repository.WriteCorrectAnswers(questions,idLevel,levelsCount);
        }
    }
}
