using Crud.Application.Features.Training.DTOs;
using Crud.Application.Features.Training.Services;
using Crud.Domain.Entities;
using Crud.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//********Create for Advance Search Features
namespace Crud.Infrastructure.Features.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IAdoNetUtility _adoNetUtility; 
        public EnrollmentService(IAdoNetUtility adoNetUtility) 
        { 
            _adoNetUtility = adoNetUtility;
        }
        public async Task<(IList<EnrollmentDTO> records, int total, int totalDisplay)>
            GetPagedEnrollmentsAsync(int pageIndex, int pageSize, string? courseName, string? userName, 
            DateTime? enrollmentDateFrom, DateTime? enrollmentDateTo, string orderBy)
        {
            var outParameters = new Dictionary<string, Type>()
            {
                { "Total", typeof(int) },
                { "TotalDisplay", typeof(int) }
            };
            var resut = await _adoNetUtility.QueryWithStoredProcedureAsync<EnrollmentDTO>
                ("GetCourseEnrollments",
                    new Dictionary<string, object>
                    {
                        {"PageIndex", pageIndex },
                        {"PageSize", pageSize},
                        {"CourseName", courseName },
                        {"UserName", userName },
                        {"EnrollmentDateFrom", enrollmentDateFrom },
                        {"EnrollmentDateTo", enrollmentDateTo },
                        {"OrderBy", orderBy }

                    },
                    outParameters);
            return (resut.result, int.Parse(resut.outValues.ElementAt(0).Value.ToString()),
                int.Parse(resut.outValues.ElementAt(1).Value.ToString()));
        }
    }
}
