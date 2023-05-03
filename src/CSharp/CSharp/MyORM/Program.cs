// See https://aka.ms/new-console-template for more information
string DbConnection = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;";

var course = new Course();


// Create a new Course object to insert into the database
//var course = new Course
//{
//    Id = Guid.NewGuid(),
//    Title = "C#",
//    Teacher = new Instructor
//    {
//        Id = Guid.NewGuid(),
//        Name = "jalal",
//        Email = "johndoe@example.com",
//        PhoneNumbers = new List<Phone> {
//            new Phone {
//                Id = Guid.NewGuid(),
//                Number = "01746902499",
//                Extension = "123",
//                CountryCode = "+1"

//            }
//        },
//        PresentAddress = new Address
//        {
//            Id = Guid.NewGuid(),
//            Street = "Nayapara",
//            City = "Anytown",
//            Country = "USA"
//        },
//        PermanentAddress = new Address
//        {
//            Id = Guid.NewGuid(),
//            Street = "Paharpur",
//            City = "Gazipur",
//            Country = "Bangladesh"
//        }

//    },
//    Topics = new List<Topic> {
//        new Topic {
//            Id = Guid.NewGuid(),
//            Title = "Geting Srarted",
//            Description = "Learn about",
//            Sessions = new List<Session>{new Session { Id = Guid.NewGuid(), DurationInHour=60, LearningObjective="abc" } }
//        },
//        new Topic {
//            Id = Guid.NewGuid(),
//            Title = "Tools Installation",
//            Description = "variables, data types, and control flow",
//            Sessions = new List<Session>{new Session { Id = Guid.NewGuid(), DurationInHour=55, LearningObjective="abc" } }
//        }
//    },
//    Tests = new List<AdmissionTest>()
//    {
//        new AdmissionTest(){Id = Guid.NewGuid(), StartDateTime = DateTime.Now, EndDateTime=DateTime.Today, TestFees=200},
//        new AdmissionTest(){Id = Guid.NewGuid(), StartDateTime = DateTime.Now, EndDateTime=DateTime.Today, TestFees=300},

//    },
//    Fees = 1000.00
//};
// Create a new instance of the MyORM class
var orm = new TestORM<Guid, Course>(DbConnection);
// Call the Insert method on the MyORM instance to insert the Course object into the database
orm.Insert(course);

//for Update
Guid myGuid = new Guid("257ae788-ad3f-4ff4-b6ea-8645071daea7");
course.Id = myGuid;
course.Title = "C++";
//orm.Update(course);
orm.GetAll();
















//Guid courseId = Guid.NewGuid();
//Guid teacherId = Guid.NewGuid();

//Course course = new Course();
//course = new Course
//{
//    Id = courseId,
//    Title = "Asp.Net",
//    Fees = 30000,
//    Topics = new List<Topic>()
//    {

//        new Topic() {Id=Guid.NewGuid(), Title = "Version Copntrol", Description = "Getting Started", CourseId = courseId },
//        new Topic() {Id = Guid.NewGuid(),   Title = "Git Hub",Description = "Tools Instal",  CourseId = courseId },
//    },
//    Teacher = new Instructor
//    {
//        Id = teacherId,
//        Name = "Jalal Uddin",
//        Email = "info@devskill.com",
//        PresentAddress = new Address
//        {
//            Id = Guid.NewGuid(),
//            Street = "17/A",
//            City = "Gazipur",
//            Country = "Bangladesh",
//            InstructorId = teacherId,
//        },
//        CourseId = courseId,
//    },
//    Tests = new List<AdmissionTest>()
//    {
//        new AdmissionTest(){Id = Guid.NewGuid(), StartDateTime = DateTime.Now, EndDateTime=DateTime.Today, TestFees=100, CourseId=courseId},
//        new AdmissionTest(){Id = Guid.NewGuid(), StartDateTime = DateTime.Now, EndDateTime=DateTime.Today, TestFees=500, CourseId=courseId},

//    }
//};



////Instructor instructor = new Instructor()
////{
////    Id = teacherId,
////    Name = "Shamim",
////    Email = "Shohaib@mondol.net",
////    PresentAddress = new Address
////    {
////        Id = Guid.NewGuid(),
////        Street = "17/A",
////        City = "Gazipur",
////        Country = "Bangladesh",
////        InstructorId = teacherId,
////    }

////};


//var orm = new TestORM<Guid, Course>(DbConnection);
//orm.Insert(course);
////orm.GetAll();
////Guid myGuid = new Guid("11a45114-275a-4ab3-9355-54dc56694427");
////orm.Delete(myGuid);
////orm.GetAll();






////var instructor = new Instructor
////{
////    Id = 1,
////    Name = "John Doe",
////    Email = "johndoe@example.com",
////    PresentAddress = new Address
////    {
////        Id= 1,
////        Street = "123 Main St",
////        City = "Anytown",
////        Country = "USA"
////    },
////    PermanentAddress = new Address
////    {
////        Id = 1,
////        Street = "456 Oak Ave",
////        City = "Sometown",
////        Country = "USA"
////    },
////    PhoneNumbers = new List<Phone>
////        {
////            new Phone
////            {
////                Id = 1,
////                Number = "555-1234",
////                Extension = "123",
////                CountryCode = "1"
////            }
////        }
////};

////var topic1 = new Topic
////{
////    Id = 1,
////    Title = "Topic 1",
////    Description = "Description of topic 1",
////    Sessions = new List<Session>
////        {
////            new Session
////            {
////                Id = 1,
////                DurationInHour = 1,
////                LearningObjective = "Objective 1"
////            },
////            new Session
////            {
////                Id = 2,
////                DurationInHour = 2,
////                LearningObjective = "Objective 2"
////            }
////        }
////};

////var topic2 = new Topic
////{
////    Id = 2,
////    Title = "Topic 2",
////    Description = "Description of topic 2",
////    Sessions = new List<Session>
////        {
////            new Session
////            {
////                Id= 1,
////                DurationInHour = 3,
////                LearningObjective = "Objective 3"
////            },
////            new Session
////            {
////                Id= 2,
////                DurationInHour = 4,
////                LearningObjective = "Objective 4"
////            }
////        }
////};

////var course = new Course
////{
////    Id = 3,
////    Title = "Course Title",
////    Teacher = instructor,
////    Topics = new List<Topic> { topic1, topic2 },
////    Fees = 1000,
////    Tests = new List<AdmissionTest>
////        {
////            new AdmissionTest
////            {
////                Id= 1,
////                StartDateTime = DateTime.Now.AddDays(7),
////                EndDateTime = DateTime.Now.AddDays(14),
////                TestFees = 50
////            },
////            new AdmissionTest
////            {
////                Id= 2,
////                StartDateTime = DateTime.Now.AddDays(21),
////                EndDateTime = DateTime.Now.AddDays(28),
////                TestFees = 50
////            }
////        }
////};
////var orm = new MyORM<int, Course>(DbConnection);













////orm.GetById(1);
////orm.GetAll();
////orm.Delete(6);
//////course.Id = 9;
//////course.Title = "Test";
//////course.IsActive = true;
//////orm.Update(course);
////orm.Delete(course);

////orm.Delete(6);
//////course.Id = 9;
//////course.Title = "Test";
//////course.IsActive = true;
//////orm.Update(course);
////orm.Delete(course);

////DataUtility dataUtility = new DataUtility(DbConnection);

