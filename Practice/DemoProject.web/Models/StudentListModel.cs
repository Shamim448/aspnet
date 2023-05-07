using DemoProject.Domain.Entities;
using DemoProject.Domain.Services;
using DemoProject.Infrastructure;

namespace DemoProject.web.Models
{
    public class StudentListModel
    {
        private readonly IStudentService _studentService;
        public StudentListModel()
        {

        }
        public StudentListModel(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public IList<Student> GetTopStudents()
        {
            return _studentService.GetStudents();
        }
        //Collect page info based on user interaction 
        public async Task<object> GetPagedStudents(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _studentService.GetPagedStudentAsync(
               dataTablesUtility.PageIndex,
               dataTablesUtility.PageSize,
               dataTablesUtility.SearchText,
               dataTablesUtility.GetSortText(new string[] { "Name", "Fee" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Email,
                                record.Phone, 
                                record.Address,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
