namespace XmlData
{
    public class Course
    {
        public string Title { get; set; }
        public Instructor Teacher { get; set; }
        public List<Topic> Topics { get; set; }
        public double Fees { get; set; }
        public List<AdmissionTest> Tests { get; set; }

        public Course()
        {
            Title = "Asp.net C#";
            Teacher = new Instructor()
            {
                Name = "Md. Jalal Uddin",
                Email = "jalaluddin@devskill.com",
                PermanentAddress = new Address()
                {
                    Street = "Moghbazar",
                    City = "Dhaka",
                    Country = "Bangladesh"
                },
                PresentAddress = new Address()
                {
                    Street = "Mirpur-2",
                    City = "Dhaka",
                    Country = "Bangladesh"
                },
                PhoneNumbers = new List<Phone>
                {
                    new Phone(){ Number = "828320328", Extension = "382", CountryCode = "555" },
                    new Phone(){ Number = "304303343", Extension = "454", CountryCode = "343" },
                }
            };

            Fees = 30000.5;

            Topics = new List<Topic>()
            {
                new Topic 
                { 
                    Title = "Gettig Started", 
                    Description = "Frist Demo", 
                    Sessions = new List<Session>
                    { 
                        new Session{ DurationInHour = 2, LearningObjective = "Start learning" },
                        new Session{ DurationInHour = 3, LearningObjective = "Write Code" },
                        new Session{ DurationInHour = 4, LearningObjective = "Run Code" },
                    }
                },
                new Topic
                {
                    Title = "Installation",
                    Description = "Tools",
                    Sessions = new List<Session>
                    {
                        new Session{ DurationInHour = 1, LearningObjective = "VS Code" },
                        new Session{ DurationInHour = 4, LearningObjective = "Docker" },
                        new Session{ DurationInHour = 2, LearningObjective = "Git" },
                    }
                },
                new Topic
                {
                    Title = "Project",
                    Description = "Build Application",
                    Sessions = new List<Session>
                    {
                        new Session{ DurationInHour = 2, LearningObjective = "Start learning" },
                        new Session{ DurationInHour = 3, LearningObjective = "Write Code" },
                        new Session{ DurationInHour = 4, LearningObjective = "Run Code" },
                    }
                },
            };

            Tests = new List<AdmissionTest>
            {
                new AdmissionTest
                {
                    TestFees = 100.5,
                    StartDateTime = new DateTime(2022, 2, 3),
                    EndDateTime = new DateTime(2022, 2, 4)
                },
                new AdmissionTest
                {
                    TestFees = 200.5,
                    StartDateTime = new DateTime(2023, 4, 3),
                    EndDateTime = new DateTime(2023, 4, 4)
                },
                new AdmissionTest
                {
                    TestFees = 300.5,
                    StartDateTime = new DateTime(2024, 5, 3),
                    EndDateTime = new DateTime(2024, 5, 4)
                }
            };
        }
    }

    public class AdmissionTest
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TestFees { get; set; }
    }

    public class Topic
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Session> Sessions { get; set; }
    }

    public class Session
    {
        public int DurationInHour { get; set; }
        public string LearningObjective { get; set; }
    }

    public class Instructor
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address PresentAddress { get; set; }
        public Address PermanentAddress { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class Phone
    {
        public string Number { get; set; }
        public string Extension { get; set; }
        public string CountryCode { get; set; }
    }
}