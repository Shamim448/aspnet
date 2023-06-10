using AutoMapper;
using Crud.Application;
using Crud.Application.Features.Training.Services;
using Crud.Domain.Entities;
using Crud.Infrastructure.Features.Exceptions;

namespace Crud.Infrastructure.Features.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public UserService(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //create user 
        public void CreateUser(string name, string email, string phone, string address)
        {
           if(_unitOfWork.Users.IsDuplicateName(name, null))
                throw new DuplicateNameException("User Name is duplicate");
           //create user 
            User user = new User() { Name = name, Email = email, Phone = phone, Address = address };          
            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
        }
        //Update user
        public User GetUser(int  id)
        {
            return _unitOfWork.Users.GetById(id);
        }
        public void UpdateUser(int id, string name, string email, string phone, string address)
        {
            if (_unitOfWork.Users.IsDuplicateName(name, id))
                throw new DuplicateNameException("User Name is duplicate");
            //create user 
            User user = _unitOfWork.Users.GetById(id);
            user.Name = name;
            user.Email = email;
            user.Phone = phone;
            user.Address = address;
            _unitOfWork.Save();
        }
        //Update user end
        //Delete
        public void DeleteUser(int id)
        {
            _unitOfWork.Users.Remove(id);
            _unitOfWork.Save();
        }
        //Data view for data table
        public IList<User> GetAllUser()
        {
            return _unitOfWork.Users.GetAll();
        }

        public async Task<(IList<User> records, int total, int totalDisplay)>
            GetPagedUserAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            return await Task.Run(() =>
            {
                var result = _unitOfWork.Users.GetDynamic(x => x.Name.Contains(searchText),
                orderBy, string.Empty, pageIndex, pageSize, true);
                return result;
            });
        }

        
        //End Data view for data table

    }
}
