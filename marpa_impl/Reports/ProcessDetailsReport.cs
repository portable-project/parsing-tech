using System;
using System.Collections.Generic;

namespace marpa_impl
{
    public struct ProcessDetailsReport
    {
        public List<EarleyItemRepresentaion> _earleyItemsList;
        public List<String> _leoItemsList;
        public bool _isParserReportValid;
        public ErrorDescription _errorDescription;

        internal ProcessDetailsReport(EarleySet earleySet)
        {
            _leoItemsList = new List<string>();
            List<LeoItem> setLeoItems = earleySet.GetLeoItemList();
            for (int i = 0; i < setLeoItems.Count; i++)
            {
                _leoItemsList.Add(setLeoItems[i].ToString());
            };

            _earleyItemsList = earleySet.GetEarleyItemReportList();
            _isParserReportValid = true;
            _errorDescription = new ErrorDescription(ErrorCode.NO_ERROR);
        }

        internal ProcessDetailsReport(ErrorDescription errorDescription)
        {
            _earleyItemsList = null;
            _leoItemsList = null;
            _isParserReportValid = false;
            _errorDescription = errorDescription;
        }
    }
}
