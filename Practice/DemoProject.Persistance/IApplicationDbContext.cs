using DemoProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Persistance
{
    public interface IApplicationDbContext
    {
        DbSet<Student> Students { get; set; }
    }
}