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
        private static string dbpath = @"C:\Users\Davidm\Desktop\quiz\quizdb.db";
        private static string insertExport = @"C:\Users\Davidm\Desktop\quiz\export.txt";

        private static string easyPath = @"C:\Users\Davidm\Desktop\quiz\easy.txt";
        private static string mediumPath = @"C:\Users\Davidm\Desktop\quiz\medium.txt";
        private static string hardPath = @"C:\Users\Davidm\Desktop\quiz\hard.txt";
        private static bool unlock = true;


        public static int LevelMAXCount = 10;

        public static List<Level> lvl;
        public static List<Question> questio;
        public static int lastLevelId = 0;
        public static int lastQuestionId = -1;
        

        //private static string 
        static void Main(string[] args)
        {
            lvl = new List<Level>();
            questio = new List<Question>();

            File.Create(dbpath).Close();
            paConnection = new SQLiteConnection("Data Source=" + dbpath + ";Version=3;");
            CreateDB();

            Parse(easyPath, "E");
            Parse(mediumPath, "M");
            Parse(hardPath, "H");

            WriteToDB();
            RunSQLs();
        }

        private static void RunSQLs()
        {
            var lines = File.ReadAllLines(insertExport);
            foreach (var line in lines) {
                runSQL(line);
            }
        }

        private static void WriteToDB()
        {
            StringBuilder sb = new StringBuilder() ;
            foreach (var l in lvl) {
                int lckd = l.IsLocked ? 1 : 0;
                string insertlvl = $"insert into LEVELS(ID,DIFFICULTY,IS_LOCKED) values({l.Id},'{l.Difficulty}',{lckd});";
                sb.AppendLine(insertlvl);
                foreach (var q in l.Questions) {
                    int isansw = q.IsAnswered ? 1 : 0;
                    string insertquest = $"insert into QUESTIONS(ID,LEVEL_ID,TEXT,IS_ANSWERED) values({q.Id},{q.LevelId},'{q.Text}',{isansw});";
                    sb.AppendLine(insertquest);
                    foreach (var o in q.Options) {
                        int corr = o.IsCorrect ? 1 : 0;
                        string insertoptions = $"insert into OPTIONS_QUESTION(TEXT,IS_CORRECT,QUESTION_ID) values('{o.Text}',{corr},{o.QuestionId});";
                        sb.AppendLine(insertoptions);
                    }
                }
            }
            sb.AppendLine("CREATE INDEX idx1 ON OPTIONS_QUESTION (QUESTION_ID,IS_CORRECT,TEXT,ID);");
            sb.AppendLine("CREATE INDEX idx2 ON QUESTIONS(LEVEL_ID, LEVEL_ID, IS_ANSWERED, TEXT);");
            File.WriteAllText(insertExport,sb.ToString(),Encoding.Unicode);
        }

        private static void CreateDB() {

            string levels = "create table LEVELS(ID Integer Not NULL PRIMARY KEY AUTOINCREMENT, DIFFICULTY TEXT Not NULL, IS_LOCKED INTEGER Not NULL)";
            string questions = "CREATE TABLE QUESTIONS (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,LEVEL_ID	INTEGER NOT NULL,IS_ANSWERED INTEGER NOT NULL, TEXT TEXT NOT NULL, FOREIGN KEY(LEVEL_ID) REFERENCES LEVELS(ID))";
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
                        
            foreach(var line in File.ReadAllLines(importedFile,Encoding.UTF8))
            {
                

                if (line.StartsWith("O:"))
                {                    
                    ++lastQuestionId;
                    string txt = line.Remove(0, 3);
                    questio.Add(new Question() { Id= lastQuestionId, Text=txt, IsAnswered=false});
                }
                else 
                {
                    if (!String.IsNullOrWhiteSpace(line)) {
                        var l = questio.Where(x=>x.Id==lastQuestionId).First();
                        if (line.StartsWith("*"))
                        {
                            var txt = line.Remove(0, 1);
                            l.Options.Add(new Options() { IsCorrect=true,QuestionId=lastQuestionId,Text=txt});
                        }
                        else
                        {
                            l.Options.Add(new Options() { IsCorrect = false, QuestionId = lastQuestionId, Text = line});
                        }
                    }
                }
            }
            CreateLevels(difficulty);
        }

        private static void CreateLevels(string difficulty)
        {
            int i = 0;
            Level lastlvl = null;
            //randomize...
            Random rand = new Random(2);
            questio = questio.OrderBy(c => rand.Next()).Select(c => c).ToList();

            foreach (var q in questio)
            {
                if (i % LevelMAXCount == 0)
                {
                    //naplnil sa mi level
                    bool lck = unlock;
                    unlock = false;
                    lastlvl = new Level() { Id = ++lastLevelId, Difficulty = difficulty,IsLocked = lck ? false:true};
                    lvl.Add(lastlvl);
                }
                q.LevelId = lastlvl.Id;
                lastlvl.Questions.Add(q);
                i++;
            }
            questio.Clear();
        }
    }


    public class Level {
        public int Id { get; set; }
        public string Difficulty { get; set; }
        public bool IsLocked { get; set; }

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
        public bool IsAnswered { get; set; }

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
