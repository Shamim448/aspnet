public class Course : IIdBase<int>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public double Fees { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
}

