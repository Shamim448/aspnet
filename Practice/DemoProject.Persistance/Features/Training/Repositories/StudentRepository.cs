using DemoProject.Application.Features.Training.Repositories;
using DemoProject.Domain.Entities;
using DemoProject.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Persistence.Features.Training.Repositories
{
	public class StudentRepository : Repository<Student, Guid>, IStudentRepository
    {
		public StudentRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
