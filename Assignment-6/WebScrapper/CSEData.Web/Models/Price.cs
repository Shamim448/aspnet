using System.ComponentModel.DataAnnotations;

namespace CSEData.Web.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public int? CompanyID { get; set; }
        public string LTP { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Volume { get; set; }
        public DateTime Time { get; set; }
    }
}
