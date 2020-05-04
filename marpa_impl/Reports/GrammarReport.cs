using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public struct GrammarReport
    {
        public List<ErrorDescription> errorDescriptions;
        public bool isSuccessfull;
        public int totalErrorCount;

        internal GrammarReport(List<ErrorDescription> _errorDescriptions)
        {
            errorDescriptions = _errorDescriptions;
            isSuccessfull = errorDescriptions == null || errorDescriptions.Count == 0;
            totalErrorCount = _errorDescriptions.Count;
        }
    }
}
