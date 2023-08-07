using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Domain
{
    public class Company:IEntity<int>
    {
        public int Id { get; set; }
        public string StockCodeName { get; set; }
        public ICollection<Price> Prices { get; }
    }
}
