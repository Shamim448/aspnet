public class Course : IIdBase<decimal>
{
    public decimal Id { get ; set ; }
    public string? Title { get; set; }
    public float Fees { get; set; }
    public bool IsActive { get; set; }
    public DateTime CourseStartDate { get; set; }
    public List<Topic> Topics { get; set; }
}
public class Topic : IIdBase<decimal>
{
    public  decimal Id { get ; set; }
    public string Title { get; set; }
    public float Duration { get; set; }
   
}

