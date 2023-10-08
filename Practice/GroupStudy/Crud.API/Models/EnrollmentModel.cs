using Autofac;
using AutoMapper;
using Crud.Application.Features.Training.Services;
using Crud.Infrastructure.Features.Services;
using DemoProject.Infrastructure;

namespace Crud.API.Models
{
    public class EnrollmentModel
    {
        private IMapper _mapper;
        private IEnrollmentService _enrollmentService;
        public EnrollmentService SearchItem { get; set; }

        public EnrollmentModel()
        {

        }
        public EnrollmentModel(IEnrollmentService enrollmentService, IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _mapper = mapper;
        }
        public void ResolveDependency(ILifetimeScope scope)
        {
            _enrollmentService = scope.Resolve<IEnrollmentService>();
            _mapper = scope.Resolve<IMapper>();
        }

        //internal async Task<object> GetPagedCoursesAdvanced(DataTablesAjaxRequestUtility dataTablesModel)
        //{
        //    var data = await _enrollmentService?.GetPagedEnrollmentsAsync(
        //        dataTablesModel.PageIndex,
        //        dataTablesModel.PageSize,
        //        SearchItem.CourseName,
        //        SearchItem.UserName,
        //        //SearchItem.enrollmentDateFrom,
        //        //SearchItem.enrollmentDateTo,
                
        //        dataTablesModel.GetSortText(new string[] { "c.Name", "u.Name", "EnrollmentDate" }));

        //    return new
        //    {
        //        recordsTotal = data.total,
        //        recordsFiltered = data.totalDisplay,
        //        data = (from record in data.records
        //                select new string[]
        //                {
        //                record.CourseName,
        //                record.UserName,
        //                record.EnrollDate.ToString()
        //                }
        //            ).ToArray()
        //    };
        //}
    }
}
