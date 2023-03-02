using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public interface IChessPiece
    {
        public string X { get;  }
        public string Y { get;  }
       public int DIMENTION { get;  }
    }
}
