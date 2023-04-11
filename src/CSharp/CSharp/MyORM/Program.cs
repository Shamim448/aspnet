// See https://aka.ms/new-console-template for more information
string DbConnection = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;";
Course course = new Course();
Guid courseId = Guid.NewGuid();
course = new Course {
    Id = courseId,
    Title = "CSharp",
    Fees = 9000,
    IsActive = false,
    CourseStartDate = new DateTime(2023, 04, 25),
    Topics = new List<Topic>()
    {

        new Topic() {Id=Guid.NewGuid(), Title = "Ado.net", Duration = 60, CourseId = courseId },
        new Topic() {Id = Guid.NewGuid(),   Title = "Entity Framework", Duration = 60, CourseId = courseId },
    }
};

var orm = new TestORM<Guid, Course>(DbConnection);
orm.Insert(course);

//var orm = new MyORM< int, Course>(DbConnection);
//orm.Insert(course);
//orm.GetById(1);
//orm.GetAll();
//orm.Delete(6);
////course.Id = 9;
////course.Title = "Test";
////course.IsActive = true;
////orm.Update(course);
//orm.Delete(course);

//DataUtility dataUtility = new DataUtility(DbConnection);