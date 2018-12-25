using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Last_layer_website_dotnet.Models
{
    public class CaseTypeInfo
    {
        public static CaseType? GetNextCaseType(CaseType c)
        {
            if (c == CaseType.OLL)
                return CaseType.OLLCP;
            if (c == CaseType.OLLCP)
                return CaseType.OneLookLL;
            return null;
        }

        public static CaseType? GetPreviousCaseType(CaseType c)
        {
            if (c == CaseType.OLLCP)
                return CaseType.OLL;
            if (c == CaseType.OneLookLL)
                return CaseType.OLLCP;
            return null;
        }
    }
}
