using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Persistence.Features.Training.Repositories
{
	public class CourseRepository : Repository<Course, Guid>, ICourseRepository
	{
		public CourseRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
