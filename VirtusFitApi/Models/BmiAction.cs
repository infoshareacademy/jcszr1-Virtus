using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public class BmiAction
    {
        public int Id { get; set; }
        public double Height { get; set; }
        public double  Weight { get; set; }
        public double  Bmi { get; set; }
        public DateTime Created { get; set; }
    }
}
