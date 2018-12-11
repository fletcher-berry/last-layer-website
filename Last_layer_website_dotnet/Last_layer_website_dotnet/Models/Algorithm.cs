using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Last_layer_website_dotnet.Models
{
    public class Algorithm
    {
        public string Alg { get; set; }
        public int? SourceNumber { get; set; }
        public bool Modified { get; set; }

        public Algorithm(string alg, int? sourceNumber, bool modified = false)
        {
            Alg = alg;
            SourceNumber = sourceNumber;
            Modified = modified;
        }
    }
}
