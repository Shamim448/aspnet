using AutoMapper;
using Crud.Domain.Entities;
using Crud.web.Areas.Admin.Models;

namespace Crud.web
{
    public class WebProfile:Profile
    {
        public WebProfile()
        {
            CreateMap<UserUpdateModel, User>()
                .ReverseMap();
        }
    }
}
