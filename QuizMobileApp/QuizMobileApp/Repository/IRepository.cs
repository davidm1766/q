using QuizMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizMobileApp.Repository
{
    public interface IRepository
    {
        List<LevelModel> GetAllLevels();

        List<QuestionModel> GetQuestions(int id);

        List<OptionInQuestionModel> GetAllOptionsForQuestion(int IdQuestion);

        JokersModel GetAllJokers();
    }
}
