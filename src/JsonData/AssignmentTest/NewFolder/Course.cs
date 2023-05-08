using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace XMLFormetter
{
    public class Course
    {
        public int id=10;
        public string Title { get; set; }
        public Instructor Teacher { get; set; }
        public List<Topic> Topics { get; set; }
        public double Fees { get; set; }
        
        public List<AdmissionTest> Tests { get; set; }
    }
}

