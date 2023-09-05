using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain.Entities
{
    public class Course : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }
        public IList<UserCourse> Users { get; set; }
    }
}
