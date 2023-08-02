using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CSEData.Domain.Entities
{
    public class Price : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int? CompanyID { get; set;}
        public double LTP { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public DateTime Time { get; set; }
    }
}
