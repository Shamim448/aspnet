using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtentionMethod
{
    public static class EntentionMethodExample
    {
        public static string AddSmaileFace(this string smaileFace)
        {
            return smaileFace + "😊";
        }
    }
}
