
using System;
using System.Net;
using System.Numerics;
using static System.Collections.Specialized.BitVector32;

public class Course : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Instructor Teacher { get; set; }
    public List<Topic> Topics { get; set; }
    public double Fees { get; set; }
    public List<AdmissionTest> Tests { get; set; }

}

public class Topic : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Session> Sessions { get; set; }
    public Guid CourseId { get; set; }
}
public class Session : IEntity<Guid>
{
    public Guid Id { get; set; }
    public int DurationInHour { get; set; }
    public string LearningObjective { get; set; }
    public Guid TopicId { get; set; }
}
public class Instructor : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Address PresentAddress { get; set; }
    public Address PermanentAddress { get; set; }
    public List<Phone> PhoneNumbers { get; set; }
    public Guid CourseId { get; set; }

}
public class AdmissionTest : IEntity<Guid>
{
    public Guid Id { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public double TestFees { get; set; }
    public Guid CourseId { get; set; }
}
public class Address : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public Guid InstructorId { get; set; }

}
public class Phone : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public string Extension { get; set; }
    public string CountryCode { get; set; }
    public Guid InstructorId { get; set; }

}






//public class Course : IIdBase<Guid>
//{

//    public Guid Id { get; set; }
//    public string Title { get; set; }
//    public Instructor Teacher { get; set; }
//    public List<Topic> Topics { get; set; }
//    public double Fees { get; set; }
//    public List<AdmissionTest> Tests { get; set; }
//}

//public class AdmissionTest : IIdBase<Guid>
//{
//    public Guid Id { get; set; }
//    public DateTime StartDateTime { get; set; }
//    public DateTime EndDateTime { get; set; }
//    public double TestFees { get; set; }
//    public Guid CourseId { get; set; }
//}

//public class Topic : IIdBase<Guid>
//{
//    public Guid Id { get; set; }
//    public string Title { get; set; }
//    public string Description { get; set; }
//    public List<Session> Sessions { get; set; }
//    public Guid CourseId { get; set; }
//}

//public class Session : IIdBase<Guid>
//{
//    public Guid Id { get; set; }
//    public Guid DurationInHour { get; set; }
//    public string LearningObjective { get; set; }
//}

//public class Instructor : IIdBase<Guid>
//{
//    public Guid Id { get; set; }
//    public string Name { get; set; }
//    public string Email { get; set; }
//    public Address PresentAddress { get; set; }
//    public Address PermanentAddress { get; set; }
//    public List<Phone> PhoneNumbers { get; set; }
//    public Guid CourseId { get; set; }

//}

//public class Address : IIdBase<Guid>
//{
//    public Guid Id { get; set; }
//    public string Street { get; set; }
//    public string City { get; set; }
//    public string Country { get; set; }
//    public Guid InstructorId { get; set; }

//}

//public class Phone : IIdBase<Guid>
//{
//    public Guid Id { get; set; }
//    public string Number { get; set; }
//    public string Extension { get; set; }
//    public string CountryCode { get; set; }
//}

