using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public struct EarleyItemReport
    {
        public String _item;
        public String _operation;

        internal EarleyItemReport(EarleyItem item, String operation)
        {
            _item = item.ToString();
            _operation = operation;
        }
    }
}
