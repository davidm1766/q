using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QuizMobileApp.Helpers;
using QuizMobileApp.Model;

namespace QuizMobileApp.Repository
{
    //public class QuestionOptionsRepository : IQuestionOptionsRepository
    //{
    //    private readonly DatabaseContext _databaseContext;
    //    public QuestionOptionsRepository(string dbPath)
    //    {
    //        _databaseContext = new DatabaseContext(dbPath);
    //    }

    //    public async Task<IEnumerable<OptionInQuestionModel>> GetAllOptionsForQuestionAsync(int IdQuestion)
    //    {
    //        try
    //        {

    //            var options = await _databaseContext.OptionInQuestionModel.FindAsync(IdQuestion);
    //            return null;
    //        }
    //        catch (Exception e) {
    //            Console.WriteLine(e.ToString());
    //            return null;
    //        }

    //    }

    //    public Task<IEnumerable<OptionInQuestionModel>> QueryAsync(Func<OptionInQuestionModel, bool> predicate)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
