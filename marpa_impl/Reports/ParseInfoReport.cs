using System;
using System.Collections.Generic;

namespace marpa_impl
{
    public struct ParseInfoReport
    {
        public List<EarleyItemReport> _earleyItemsList;
        public List<String> _leoItemsList;
        public bool _isParserReportValid;
        public ErrorDescription _errorDescription;

        internal ParseInfoReport(EarleySet earleySet)
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

        internal ParseInfoReport(ErrorDescription errorDescription)
        {
            _earleyItemsList = null;
            _leoItemsList = null;
            _isParserReportValid = false;
            _errorDescription = errorDescription;
        }
    }
}
