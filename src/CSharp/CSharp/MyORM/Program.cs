// See https://aka.ms/new-console-template for more information
string DbConnection = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;";
Courses course = new Courses();
course = new Courses {
    Title = "CSharp",
    Fees = 9000,
    IsActive = false,
    CourseStartDate = new DateTime(2023, 04, 25),
    Topics = new List<Topic>()
    {
        new Topic() { Title = "Geting Trarted", Duration = 45.00 },
        new Topic() { Title = "Tools Installation", Duration = 45.00  },
    }
};

var orm = new TestORM<int, Courses>(DbConnection);
orm.Insert(course);

//var orm = new MyORM< int, Course>(DbConnection);
//orm.Insert(course);
////orm.GetById(10);
////orm.Delete(10);
////course.Id = 9;
////course.Title = "Test";
////course.IsActive = true;
////orm.Update(course);
////orm.Delete(course);
//orm.GetAll();
//DataUtility dataUtility = new DataUtility(DbConnection);