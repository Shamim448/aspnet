// See https://aka.ms/new-console-template for more information
using Assignment1;

Course course = new Course
{
   
    Title = "Asp.Net",
    Teacher = new Instructor
    {
        Name = "Jalal Uddin",
        Email = "info@devskill.com",
        PresentAddress = new Address
        {
            Street = "27/A",
            City = "Dhaka",
            Country = "Bangladesh",
        },
        PermanentAddress = new Address
        {
            Street = "100/02 Bogura",
            City = "Bogura",
            Country = "Bangladesh",
        },
        PhoneNumbers = new List<Phone>
        {
            new Phone { Number = 01746902499, Extension = "1137", CountryCode = "+88" },
            new Phone { Number = 01954440838, Extension = "1049", CountryCode = "+88" }
        },
    }, //End of Teacher
    Topics = new List<Topic>
    {
        new Topic
        {
            Title = "Geting started",
            Description = "Tools Installation and IDE Setup",
            Sessions = new List<Session>
            {
                new Session { DurationInHour = 2, LearningObjective = "Intruduceing" },
                new Session { DurationInHour = 2, LearningObjective = "Software Installation" },
            },

        },
        new Topic
        {
            Title = "Verssion Control",
            Description = "Verssion Controling system Git, Github",
            Sessions = new List<Session>
            {
                new Session { DurationInHour = 2, LearningObjective = "Branch create" },
                new Session { DurationInHour = 2, LearningObjective = "Git Flow" },
            },
        }
    },//End of Topics
    Fees = 3000.00,
    Tests = new List<AdmissionTest>
    {
        new AdmissionTest
        {
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now.AddHours(2),
            TestFees = 100.00,
        },
    } //end of tests
};//end of course

string json = XmlFormatter.Convert(course);
Console.WriteLine(json);
