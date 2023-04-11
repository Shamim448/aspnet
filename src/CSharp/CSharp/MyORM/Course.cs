using System;

public class Course : IIdBase<Guid>
{
    public  Guid Id { get ; set ; }
    public string? Title { get; set; }
    public float Fees { get; set; }
    public bool IsActive { get; set; }
    public DateTime CourseStartDate { get; set; }
    public List<Topic> Topics { get; set; }
    public Instructor Teacher { get; set; }
}
public class Topic : IIdBase<Guid>
{
    public  Guid Id{ get ; set; }
    public string Title { get; set; }
    public float Duration { get; set; }
    public Guid CourseId { get; set; }
}
public class Instructor : IIdBase<Guid>
{
    public Guid Id { get ; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid CourseId { get; set; }
}

