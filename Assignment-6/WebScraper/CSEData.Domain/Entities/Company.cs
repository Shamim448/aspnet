using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Domain.Entities
{
    public class Company : IEntity<int>
    {
        public int Id { get ; set ; }
        public string StockCodeName { get; set; }
    }
}
