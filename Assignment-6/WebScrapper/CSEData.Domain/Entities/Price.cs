using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Domain
{
    public class Price : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string LTP { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Volume { get; set; }
        public DateTime Time { get; set; }
        public Company Company { get; set; }
    }
}
