using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QuizMobileApp.Model;
using QuizMobileApp.Repository;

namespace QuizMobileApp.Droid.Database
{
    public class Repository : IRepository
    {
        private SQLiteUtil _sqliteUtil;
        public Repository(SQLiteUtil util) {
            _sqliteUtil = util;
        }

        public List<LevelModel> GetAllLevels()
        {
            List<LevelModel> ret = new List<LevelModel>();
            SQLiteDatabase _objSQLiteDatabase = _sqliteUtil.WritableDatabase;
            ICursor c = _objSQLiteDatabase.RawQuery("SELECT * FROM LEVELS ", new string[] { });

            if (c.Count > 0)
            {
                c.MoveToFirst();
                do
                {
                    int id = c.GetInt(c.GetColumnIndex("ID"));
                    var questions = this.GetQuestions(id);
                    ret.Add(new LevelModel(questions) { IdLevel=id,AllQuestionsCount=questions.Count,CorrectAnswersCount = 0 });
                } while (c.MoveToNext());
                c.Close();
            }
            return ret;
        }

        public List<QuestionModel> GetQuestions(int id)
        {
            List<QuestionModel> ret = new List<QuestionModel>();
            SQLiteDatabase _objSQLiteDatabase = _sqliteUtil.WritableDatabase;
            ICursor c = _objSQLiteDatabase.RawQuery("SELECT * FROM QUESTIONS WHERE LEVEL_ID = ?", new string[] { id.ToString() });

            if (c.Count > 0)
            {
                c.MoveToFirst();
                do
                {
                    int idquestion = c.GetInt(c.GetColumnIndex("ID"));
                    int levelID = c.GetInt(c.GetColumnIndex("LEVEL_ID"));
                    string text = c.GetString(c.GetColumnIndex("TEXT"));
                    List<OptionInQuestionModel> options = GetAllOptionsForQuestion(idquestion);
                    ret.Add(new QuestionModel(options) { IdQuestion = idquestion, QuestionText = text, LevelId = levelID });
                } while (c.MoveToNext());
                c.Close();
            }
            return ret;
        }

        public List<OptionInQuestionModel> GetAllOptionsForQuestion(int IdQuestion)
        {
            List<OptionInQuestionModel> ret = new List<OptionInQuestionModel>();
            SQLiteDatabase _objSQLiteDatabase = _sqliteUtil.WritableDatabase;
            ICursor c = _objSQLiteDatabase.RawQuery("SELECT * FROM OPTIONS_QUESTION WHERE QUESTION_ID = ?", new string[] { IdQuestion.ToString() });
            
            if (c.Count > 0)
            {
                c.MoveToFirst();
                do
                {
                    int id = c.GetInt(c.GetColumnIndex("ID"));
                    string text = c.GetString(c.GetColumnIndex("TEXT"));
                    bool isCorrect = c.GetInt(c.GetColumnIndex("IS_CORRECT")) == 0 ? false : true;
                    int questionID = c.GetInt(c.GetColumnIndex("QUESTION_ID"));

                    ret.Add(new OptionInQuestionModel() { IdOption = id, IsCorrect = isCorrect, Text = text, IdQuestion = questionID});
                } while (c.MoveToNext());
                c.Close();
            }
            return ret;
        }
        
    }
}