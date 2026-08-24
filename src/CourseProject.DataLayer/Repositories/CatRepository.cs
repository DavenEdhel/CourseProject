using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CourseProject.DataLayer.Models;

namespace CourseProject.DataLayer.Repositories;

public sealed class CatRepository : ICatRepository
{
    public IReadOnlyList<Cat> GetAll()
    {
        using var context = DbContextFactory.Create();
        return context.Cats.AsNoTracking().ToList();
    }

    public void Save(Cat cat)
    {
        using var context = DbContextFactory.Create();
        if (cat.Id == 0)
        {
            context.Cats.Add(cat);
        }
        else
        {
            context.Cats.Update(cat);
        }

        context.SaveChanges();
    }

    public void Delete(int id)
    {
        using var context = DbContextFactory.Create();
        var cat = context.Cats.Find(id);
        if (cat is not null)
        {
            context.Cats.Remove(cat);
            context.SaveChanges();
        }
    }
}
