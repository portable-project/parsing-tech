using System.Collections.Generic;

namespace marpa_impl
{
    public struct RecogniserReport
    {
        public List<ErrorDescription> errorDescriptions;
        public bool isRecognised;
        public bool isSuccessfull;
        internal RecogniserReport(bool _isRecognised, List<ErrorDescription> _errorDescriptions)
        {
            errorDescriptions = _errorDescriptions;
            isSuccessfull = _errorDescriptions.Count == 0;
            isRecognised = isSuccessfull ? _isRecognised : false;
        }
    }
}
