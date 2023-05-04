using DemoProject.Domain.Entities;

namespace DemoProject.Domain.Entities
{
    public class Student : IEntity<Guid>
    {    
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
