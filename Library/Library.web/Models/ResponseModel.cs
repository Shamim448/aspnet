namespace Library.web.Models
{
    public enum ResponseType
    {
        Success,
        Danger
    }
      
        public class ResponseModel
        {
            public string? Message { get; set; }
            public ResponseType Type { get; set; }
        }
   
}
