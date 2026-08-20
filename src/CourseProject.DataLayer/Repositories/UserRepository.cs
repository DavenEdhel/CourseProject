using CourseProject.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User TryGet(string name, string password)
        {
            using var context = DbContextFactory.Create();
            var user = context.Users.AsNoTracking().FirstOrDefault(x => x.Login == name && x.Password == password);
            return user;
        }
    }
}