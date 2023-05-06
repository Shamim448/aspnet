using DemoProject.Domain.Entities;

namespace DemoProject.Domain.Entities
{
    public class Student : IEntity<Guid>
    {    
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

    }
}
