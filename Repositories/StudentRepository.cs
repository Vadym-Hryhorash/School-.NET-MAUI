using Lab3_Programming.Data;
using Lab3_Programming.Models;

namespace Lab3_Programming.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _db;

        public StudentRepository(StudentDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<List<Student>> GetStudents()
        {
            return await _db.Connection.Table<Student>().ToListAsync();
        }
        public async Task<Student> GetById(int id)
        {
            return await _db.Connection.Table<Student>().Where(x => x.StudentId == id).FirstOrDefaultAsync();
        }
        public async Task Create(Student student)
        {
            await _db.Connection.InsertAsync(student);
        }
        public async Task Update(Student student)
        {
            await _db.Connection.UpdateAsync(student);
        }
        public async Task Delete(Student student)
        {
            await _db.Connection.DeleteAsync(student);
        }

    }
}
