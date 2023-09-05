using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain.Entities
{
    public class UserCourse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public User User { get; set; }
        public Course Course { get; set;}
        public DateTime EnrollDate { get; set; }
    }
}
