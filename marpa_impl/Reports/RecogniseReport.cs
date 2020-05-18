using System.Collections.Generic;

namespace marpa_impl
{
    public struct RecogniseReport
    {
        public List<ErrorDescription> errorDescriptions;
        public bool isRecognised;
        public bool isSuccessfull;
        internal RecogniseReport(bool _isRecognised, List<ErrorDescription> _errorDescriptions)
        {
            errorDescriptions = _errorDescriptions;
            isSuccessfull = _errorDescriptions.Count == 0;
            isRecognised = isSuccessfull ? _isRecognised : false;
        }
    }
}
