using DemoProject.Domain.Entities;

namespace DemoProject.Domain.Entities
{
    public class Student : IEntity<Guid>
    {
        public string? Name { get; set; }
        public Guid Id { get; set; }    
    }
}
