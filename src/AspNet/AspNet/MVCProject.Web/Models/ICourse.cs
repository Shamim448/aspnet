namespace MVCProject.Web.Models
{
    public interface ICourse
    {
        string Name { get; }
        string Description { get; }
        public void GetCourse();
    }
}
