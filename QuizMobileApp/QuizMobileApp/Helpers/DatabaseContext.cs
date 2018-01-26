using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizMobileApp.Helpers
{
    public class DatabaseContext: DbContext
    {

        private readonly string _databasePath;

        public DatabaseContext(string databasePath)
        {
            _databasePath = databasePath;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBulider)
        {
            optionsBulider.UseSqlite($"Filename={_databasePath}");
        }

    }
}
