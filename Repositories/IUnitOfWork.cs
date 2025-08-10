namespace StudentManagementSystem.Repositories
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        Task<int> SaveAsync();
    }
}
