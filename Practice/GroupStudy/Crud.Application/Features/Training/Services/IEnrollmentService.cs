using Crud.Application.Features.Training.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//********Create for Advance Search Features
namespace Crud.Application.Features.Training.Services
{
    public interface IEnrollmentService
    {
        Task<(IList<EnrollmentDTO> records, int total, int totalDisplay)>
            GetPagedEnrollmentsAsync(int pageIndex, int pageSize, string? courseName, string? userName,
            DateTime? enrollmentDateFrom, DateTime? enrollmentDateTo, string orderBy);
    }
}
