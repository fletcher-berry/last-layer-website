using Last_layer_website_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Last_layer_website_dotnet.Database
{
    public interface IAlgorithmService
    {
        AlgPage GetOll();
        AlgPage GetLastLayer(CaseType caseType, int id);
    }
}
