public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Instructor Teacher { get; set; }
}
public class Instructor
{
    public string Name { get; set; }
}
