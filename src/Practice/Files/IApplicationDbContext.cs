using FirstDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; set; }
    }
}