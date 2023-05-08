namespace Crud.Domain.Entities
{
    public class Course:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }
    }
}
