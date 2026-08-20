using System.Linq;
using Microsoft.EntityFrameworkCore;
using CourseProject.DataLayer.Models;

namespace CourseProject.DataLayer.Repositories;

public sealed class CounterRepository : ICounterRepository
{
    public Counter Get()
    {
        using var context = DbContextFactory.Create();
        var counter = context.Counters.AsNoTracking().FirstOrDefault();
        if (counter is null)
        {
            counter = new Counter { Value = 0 };
            Save(counter);
        }

        return counter;

    }

    public void Save(Counter counter)
    {
        using var context = DbContextFactory.Create();
        if (counter.Id == 0)
        {
            context.Counters.Add(counter);
        }
        else
        {
            context.Counters.Update(counter);
        }

        context.SaveChanges();
    }
}
