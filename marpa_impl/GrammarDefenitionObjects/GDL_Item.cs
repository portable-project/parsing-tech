using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    public struct GDL_Item
    {
        private GDL_Type _type;
        private string _regexp;

        public GDL_Item(GDL_Type type, string regex)
        {
            _type = type;
            _regexp = regex;
        }

        public string getRegex()
        {
            return _regexp;
        }

        public GDL_Type getType()
        {
            return _type;
        }
    }
}
