using Crud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance.DataSeeding
{
    internal static class StudentSeed
    {
        public static IList<Student> Students
        {
            get
            {
                return new List<Student>()
                {
                    new Student { Id=1, Name="Saba", Address = "Naogaon"},
                    new Student { Id=2, Name="Fatema", Address = "Naogaon"}
                };
            }
        }
    }
}
