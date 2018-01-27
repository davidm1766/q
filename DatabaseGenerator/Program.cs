using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace DatabaseGenerator
{
    class Program
    {
        private static SQLiteConnection paConnection;
        private static string dbpath = @"C:\Users\Davidm\Desktop\quiz\quizdb1.db";

        private static string easyPath = @"C:\Users\Davidm\Desktop\quiz\easy.txt";
        private static string mediumPath = @"C:\Users\Davidm\Desktop\quiz\medium.txt";
        private static string hardPath = @"C:\Users\Davidm\Desktop\quiz\hard.txt";


        public static int LevelMAXCount = 10;

        public static List<Level> lvl;
        public static int lastLevelId = -1;
        public static int lastQuestionId = -1;

        //private static string 
        static void Main(string[] args)
        {
            lvl = new List<Level>();
            File.Create(dbpath).Close();
            paConnection = new SQLiteConnection("Data Source=" + dbpath + ";Version=3;");
            CreateDB();
            Parse(easyPath, "E");
            Parse(mediumPath, "M");
            Parse(hardPath, "H");
        }

        private static void CreateDB() {

            string levels = "create table LEVELS(ID Integer Not NULL PRIMARY KEY AUTOINCREMENT, DIFFICULTY TEXT Not NULL)";
            string questions = "CREATE TABLE QUESTIONS (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,LEVEL_ID	INTEGER NOT NULL, TEXT TEXT NOT NULL, FOREIGN KEY(LEVEL_ID) REFERENCES LEVELS(ID))";
            string options = "CREATE TABLE `OPTIONS_QUESTION` (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,TEXT TEXT NOT NULL,IS_CORRECT INTEGER NOT NULL,QUESTION_ID INTEGER NOT NULL,FOREIGN KEY(QUESTION_ID) REFERENCES QUESTIONS(ID))";
            runSQL(levels);
            runSQL(questions);
            runSQL(options);
        }

        private static void runSQL(string sql)
        {
            var command = new SQLiteCommand(sql, paConnection);
            try
            {
                paConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                command.Dispose();
                paConnection.Close();
            }
        }


        public static void Parse(string importedFile,string difficulty)
        {            
                        
            foreach(var line in File.ReadAllLines(importedFile))
            {
                

                if (line.StartsWith("O:"))
                {                    
                    ++lastQuestionId;

                    if (lastQuestionId % LevelMAXCount == 0)
                    {
                        //naplnil sa mi level
                        lvl.Add(new Level() { Id = ++lastLevelId, Difficulty = difficulty });
                    }

                    string txt = line.Remove(0, 3);
                    var l = lvl.Where(x => x.Id == lastLevelId).First();
                    l.Questions.Add(new Question() { Id = lastQuestionId, LevelId = lastLevelId, Text = txt });
                }
                else 
                {
                    if (!String.IsNullOrWhiteSpace(line)) {
                        var l = lvl.Where(x => x.Id == lastLevelId).First().Questions.Where(x=>x.Id==lastQuestionId).First();
                        if (line.StartsWith("*"))
                        {
                            var txt = line.Remove(0, 1);
                            l.Options.Add(new Options() { IsCorrect=true,QuestionId=lastQuestionId,Text=txt});
                        }
                        else {
                            l.Options.Add(new Options() { IsCorrect = false, QuestionId = lastQuestionId, Text = line });
                        }
                    }
                }
            }
            var xa = lvl;
        }
        
    }


    public class Level {
        public int Id { get; set; }
        public string Difficulty { get; set; }

        public List<Question> Questions { get; set; }

        public Level() {
            Questions = new List<Question>();
        }
    }

    public class Question {
        public int Id { get; set; }
        public string Text { get; set; }
        public int LevelId { get; set; }
        public List<Options> Options { get; set; }

        public Question() {
            Options = new List<Options>();
        }
    }

    public class Options {
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
    }
}
