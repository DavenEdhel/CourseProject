using CourseProject.DataLayer.Models;

namespace CourseProject.DataLayer.BusinessModels
{
    public class CurrentUser
    {
        public static CurrentUser Instance { get; } = new CurrentUser();

        public Role Role { get; set; } = Role.User;

        public void Set(User user)
        {
            Role = user.Role switch
            {
                "Admin" => Role.Admin,
                "ContentManager" => Role.ContentManager,
                "User" => Role.User,
                _ => Role.User
            };
        }
    }
}