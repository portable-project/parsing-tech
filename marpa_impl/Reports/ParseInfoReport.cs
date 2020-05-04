using System;
using System.Collections.Generic;

namespace marpa_impl
{
    public struct ParseInfoReport
    {
        public List<String> _earleyItemsList;
        public List<String> _leoItemsList;
        public bool _isParserReportValid;
        public ErrorDescription _errorDescription;

        internal ParseInfoReport(EarleySet earleySet)
        {
            _earleyItemsList = new List<string>();
            _leoItemsList = new List<string>();

            List<EarleyItem> setEarleyItems = earleySet.GetEarleyItemList();
            List<LeoItem> setLeoItems = earleySet.GetLeoItemList();

            for(int i = 0; i< setEarleyItems.Count; i++)
            {
                _earleyItemsList.Add(setEarleyItems[i].ToString());
            };

            for (int i = 0; i < setLeoItems.Count; i++)
            {
                _leoItemsList.Add(setLeoItems[i].ToString());
            };

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
