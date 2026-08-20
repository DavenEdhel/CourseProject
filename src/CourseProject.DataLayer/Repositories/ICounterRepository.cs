using CourseProject.DataLayer.Models;

namespace CourseProject.DataLayer.Repositories;

public interface ICounterRepository
{
    Counter Get();

    void Save(Counter counter);
}
