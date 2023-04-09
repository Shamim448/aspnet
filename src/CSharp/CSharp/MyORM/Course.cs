public class Courses : IIdBase<int>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public double Fees { get; set; }
    public bool IsActive { get; set; }
    public DateTime CourseStartDate { get; set; }
    public List<Topic> Topics { get; set; }
}
public class Topic : IIdBase<int>
{
    public int Id { get ; set; }
    public string Title { get; set; }
    public double Duration { get; set; }

}

