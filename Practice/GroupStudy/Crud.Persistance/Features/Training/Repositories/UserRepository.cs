﻿using Crud.Application.Features.Training.Repositories;
using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance.Features.Training.Repositories
{
    public abstract class UserRepository : Repository<User, int>, IUserRepository
    {
        protected UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
