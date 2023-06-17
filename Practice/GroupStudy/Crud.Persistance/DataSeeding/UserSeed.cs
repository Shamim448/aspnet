using Crud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance.DataSeeding
{
    internal static class UserSeed
    {
        public static IList<User> Users
        {
            get
            {
                return new List<User>()
                {
                    new User { Id= new Guid("A41D000C-5FB1-47B7-ADDF-F0C73FAEBEEA"), Name="Saba", Email="Saba@gmail.com", Phone="01746902499", Address = "Naogaon"},
                    
                    
                };
            }
        }
    }
}
