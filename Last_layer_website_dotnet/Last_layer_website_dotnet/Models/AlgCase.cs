using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Last_layer_website_dotnet.Models
{
    public class AlgCase
    {
        public CaseType CaseType { get; private set; }
        public int CaseNumber { get; set; }
        public List<Algorithm> Algorithms { get; set; }
        public CaseType? NextCaseType { get; }

        public AlgCase(CaseType caseType, int caseNumber)
        {
            CaseType = caseType;
            NextCaseType = CaseTypeInfo.GetNextCaseType(caseType);
            CaseNumber = caseNumber;
            Algorithms = new List<Algorithm>();
        }
    }
}
