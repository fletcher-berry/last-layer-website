using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Last_layer_website_dotnet.Models
{
    public class Source
    {
        public string SourceText;
        public string GeneralSourceText;
        public string Url;

        public Source(string source, string generalSource, string url)
        {
            SourceText = source;
            GeneralSourceText = generalSource;
            Url = url;
        }
        //public int Idx;
    }
}
