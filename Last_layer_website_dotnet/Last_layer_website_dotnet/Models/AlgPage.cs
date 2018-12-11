using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Last_layer_website_dotnet.Models
{
    public class AlgPage
    {
        public List<AlgCase> Cases;
        public List<Source> Sources;

        public AlgPage(List<AlgCase> cases, List<Source> sources)
        {
            Cases = cases;
            Sources = sources;
        }
    }
}
