using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Student student)
        {
            _appDbContext.Students.Add(student);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var student = await _appDbContext.Students.FindAsync(id);
            if(student != null)
            {
                _appDbContext.Remove(student);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Student> GetStudentById(int id)
        {
           return await _appDbContext.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _appDbContext.Students.ToListAsync();
        }

        public async Task Update(Student student)
        {
            _appDbContext.Entry(student).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

        }
    }
}
