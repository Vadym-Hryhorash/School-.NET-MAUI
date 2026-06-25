using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Programming.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int StudentId { get; set; }

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? ThirdName { get; set; }

        public double? AverageGrade { get; set; }

        public string? ParentsPhone { get; set; }

        public string? FavoriteSubject { get; set; }
    }
}
