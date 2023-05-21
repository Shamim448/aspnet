using Crud.Domain.Entities;
using Crud.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public UserService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IList<User> GetAllUser()
        {
            return _unitOfWork.Users.GetAll();
        }
    }
}
