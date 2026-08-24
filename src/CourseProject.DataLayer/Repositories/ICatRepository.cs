using System.Collections.Generic;
using CourseProject.DataLayer.Models;

namespace CourseProject.DataLayer.Repositories;

public interface ICatRepository
{
    IReadOnlyList<Cat> GetAll();

    void Save(Cat cat);

    void Delete(int id);
}
