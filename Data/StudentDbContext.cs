using Lab3_Programming.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Programming.Data
{
    public class StudentDbContext
    {
        private const string DB_NAME = "student_db.db3";
        public SQLiteAsyncConnection Connection { get; private set; }

        public StudentDbContext()
        {
            Connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            Connection.CreateTableAsync<Student>();
        }

    }
}
