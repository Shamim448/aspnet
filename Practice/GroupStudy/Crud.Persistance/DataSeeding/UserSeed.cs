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
                    new User { Id=1, Name="Saba", Email="Saba@gmail.com", Phone="01746902499", Address = "Naogaon"},
                    new User { Id=2, Name="Fatema", Email="Saba@gmail.com", Phone="01746902499", Address = "Dhaka"},
                    
                };
            }
        }
    }
}
