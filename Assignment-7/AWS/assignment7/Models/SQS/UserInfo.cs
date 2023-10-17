using Amazon.SQS;

namespace assignment7.Models.SQS
{
    public class UserInfo
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

    }
    
}
