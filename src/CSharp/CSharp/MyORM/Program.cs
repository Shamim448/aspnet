// See https://aka.ms/new-console-template for more information
string DbConnection = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;";
Course course = new Course();
course = new Course { Title = "CSharp", Fees = 30000, IsActive = true, StartDate = DateTime.Now };
var orm = new MyORM< int, Course>(DbConnection);
//orm.Insert(course);
orm.GetAll();
