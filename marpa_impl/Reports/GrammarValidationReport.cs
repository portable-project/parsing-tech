using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public struct GrammarValidationReport
    {
        public List<ErrorDescription> errorDescriptions;
        public bool isSuccessfull;
        public int totalErrorCount;

        internal GrammarValidationReport(List<ErrorDescription> _errorDescriptions)
        {
            errorDescriptions = _errorDescriptions;
            isSuccessfull = errorDescriptions == null || errorDescriptions.Count == 0;
            totalErrorCount = _errorDescriptions.Count;
        }
    }
}
