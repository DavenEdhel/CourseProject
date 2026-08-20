using CourseProject.DataLayer.Models;

namespace CourseProject.DataLayer.Repositories
{
    public interface IUserRepository
    {
        User TryGet(string name, string password);
    }
}