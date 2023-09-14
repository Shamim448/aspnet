using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application.Features.Training.DTOs
{
    public class EnrollmentDTO
    {
        public string CourseName { get; set; }
        public string UserName { get; set; }
        public DateTime EnrollDate { get; set; }
    }
}
