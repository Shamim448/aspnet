using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Infrastructure.Extentions
{
    internal class MarketClosedExceptions: Exception
    {
        public MarketClosedExceptions(string message) : base(message) { }
    }
    
}
