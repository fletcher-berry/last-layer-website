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
        public CaseType CaseType { get; private set; }
        public CaseType? NextCaseType { get; }
        public CaseType? PreviousCaseType { get; }
        public int SuperCaseId;

        // mapping from case types to their names as shown in the UI
        public readonly Dictionary<CaseType, string> CaseTypeMap = new Dictionary<CaseType, string>()
        { { CaseType.OLL, "OLL"}, {CaseType.OLLCP, "OLLCP" }, {CaseType.OneLookLL, "1LLL"}, {CaseType.PLL, "PLL" } };

        public AlgPage(List<AlgCase> cases, List<Source> sources, CaseType c, int superCaseId = -1)
        {
            Cases = cases;
            Sources = sources;
            CaseType = c;
            NextCaseType = CaseTypeInfo.GetNextCaseType(c);
            PreviousCaseType = CaseTypeInfo.GetPreviousCaseType(c);
            SuperCaseId = superCaseId;
        }

        public CaseType? GetNextCaseType()
        {
            if (CaseType == CaseType.OLL)
                return CaseType.OLLCP;
            if (CaseType == CaseType.OLLCP)
                return CaseType.OneLookLL;
            return null;

        }
    }
}
