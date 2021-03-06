﻿using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using QuizMobileApp.Model;
using QuizMobileApp.Repository;

namespace QuizMobileApp.Droid.Database
{
    public class Repository : IRepository
    {
        private SQLiteUtil _sqliteUtil;
        private SQLiteDatabase _objSQLiteDatabase;
        public Repository(SQLiteUtil util)
        {
            _sqliteUtil = util;
            _objSQLiteDatabase = _sqliteUtil.WritableDatabase;
        }

        public JokersModel GetAllJokers() {
            return new JokersModel() { Joker50on50 = 1, JokerPeople = 1, JokerPhone = 1 };
        }

        public List<LevelModel> GetAllLevels()
        {
            List<LevelModel> ret = new List<LevelModel>();
            _objSQLiteDatabase = _sqliteUtil.WritableDatabase;
            ICursor c = _objSQLiteDatabase.RawQuery("SELECT * FROM LEVELS ", new string[] { });

            if (c.Count > 0)
            {
                c.MoveToFirst();
                do
                {
                    int id = c.GetInt(c.GetColumnIndex("ID"));
                    var lckd = c.GetInt(c.GetColumnIndex("IS_LOCKED")) == 0 ? false : true;
                    var questions = this.GetQuestions(id);
                    ret.Add(new LevelModel(questions) { IdLevel=id,AllQuestionsCount=questions.Count,IsLocked = lckd });
                } while (c.MoveToNext());
                c.Close();
            }
            return ret;
        }

        public List<QuestionModel> GetQuestions(int id)
        {
            List<QuestionModel> ret = new List<QuestionModel>();
           // SQLiteDatabase _objSQLiteDatabase = _sqliteUtil.WritableDatabase;
            ICursor c = _objSQLiteDatabase.RawQuery("SELECT * FROM QUESTIONS WHERE LEVEL_ID = ?", new string[] { id.ToString() });

            if (c.Count > 0)
            {
                c.MoveToFirst();
                do
                {
                    int idquestion = c.GetInt(c.GetColumnIndex("ID"));
                    int levelID = c.GetInt(c.GetColumnIndex("LEVEL_ID"));
                    string text = c.GetString(c.GetColumnIndex("TEXT"));
                    bool isanswered = c.GetInt(c.GetColumnIndex("IS_ANSWERED")) == 0 ? false : true;
                    
                    List<OptionInQuestionModel> options = GetAllOptionsForQuestion(idquestion);
                    ret.Add(new QuestionModel(options) { IdQuestion = idquestion, QuestionText = text, LevelId = levelID,IsAnswered = isanswered });
                } while (c.MoveToNext());
                c.Close();
            }
            return ret;
        }
        
        public List<OptionInQuestionModel> GetAllOptionsForQuestion(int IdQuestion)
        {
            List<OptionInQuestionModel> ret = new List<OptionInQuestionModel>();
            //SQLiteDatabase _objSQLiteDatabase = db;//_sqliteUtil.WritableDatabase;
            
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

        public void WriteCorrectAnswers(List<QuestionModel> questions,int idLevel,int maxLevel)
        {
            SQLiteDatabase _objSQLiteDatabase = _sqliteUtil.WritableDatabase;
            
            foreach (var record in questions)
            {
                ContentValues cv = new ContentValues();
                cv.Put("IS_ANSWERED", record.IsAnswered ? 1 : 0);
                _objSQLiteDatabase.Update("QUESTIONS", cv, $"ID={record.IdQuestion}", null);
            }
            //unlock next level
            if (maxLevel < idLevel + 1)
            {
                //cannot increment MAX
            }
            else {
                ContentValues cv = new ContentValues();
                cv.Put("IS_LOCKED", 0);
                _objSQLiteDatabase.Update("LEVELS", cv, $"ID={idLevel+1}", null);
            }
            
        }

        public int GetActualVersion()
        {
            
            //KEY | VALUE => string, string
            ICursor c = _objSQLiteDatabase.RawQuery("SELECT * FROM SETTING WHERE KEY = ?", new string[] { "VERSION" });
            int ret = -1;
            if (c.Count > 0)
            {
                c.MoveToFirst();
                do
                {
                    ret = int.Parse(c.GetString(c.GetColumnIndex("VALUE")));
                } while (c.MoveToNext());
                c.Close();
            }
            return ret;
        }

        public void SetActualVersion(int version)
        {
           // SQLiteDatabase _objSQLiteDatabase = _sqliteUtil.WritableDatabase;

            ContentValues cv = new ContentValues();
            cv.Put("VALUE", version.ToString());
            _objSQLiteDatabase.Update("SETTING", cv, $"KEY='VERSION'", null);
            
        }
    }
}