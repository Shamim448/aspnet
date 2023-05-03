namespace DemoProject.web.Models
{
    public class Student : IStudent
    {
        public string? Name { get; set; }
        public int Id { get; set; }   
        public void GetStudent() {
            Name = "Shamim";
            Id = 1;
            Console.WriteLine(Name+" " + Id);
        }
    }
}
