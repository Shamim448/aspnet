using System;
using System.Net;

public class Course : IIdBase<int>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Instructor Teacher { get; set; }
    public List<Topic> Topics { get; set; }
    public double Fees { get; set; }
    public List<AdmissionTest> Tests { get; set; }
}

public class AdmissionTest : IIdBase<int>
{
    public int Id { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public double TestFees { get; set; }
}

public class Topic : IIdBase<int>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Session> Sessions { get; set; }
}

public class Session : IIdBase<int>
{
    public int Id { get; set; }
    public int DurationInHour { get; set; }
    public string LearningObjective { get; set; }
}

public class Instructor : IIdBase<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Address PresentAddress { get; set; }
    public Address PermanentAddress { get; set; }
    public List<Phone> PhoneNumbers { get; set; }
}

public class Address : IIdBase<int>
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}

public class Phone : IIdBase<int>
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string Extension { get; set; }
    public string CountryCode { get; set; }
}


