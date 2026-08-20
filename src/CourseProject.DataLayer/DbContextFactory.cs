using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataLayer;

internal static class DbContextFactory
{
    private const string ConnectionString =
        @"Server=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Work\Others\Курсач\src\CourseProject.UI\Data\CourseProjectDb.mdf;Database=CourseProjectDb;Trusted_Connection=True;TrustServerCertificate=True";

    public static CourseProjectDbContext Create()
    {
        var options = new DbContextOptionsBuilder<CourseProjectDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        return new CourseProjectDbContext(options);
    }
}
