using Lab3_Programming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Programming.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetStudents();
        public Task<Student> GetById(int id);
        public Task Create(Student student);
        public Task Update(Student student);
        public Task Delete(Student student);
    }
}
