using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    internal struct GDL_Item
    {
        private GDL_Type _type;
        private string _regexp;
        private List<GDL_Type> _tokenList;

        internal GDL_Item(GDL_Type type, string regex)
        {
            _type = type;
            _regexp = regex;
            _tokenList = null;
        }
        internal GDL_Item(GDL_Type type, string regex, List<GDL_Type> tokens)
        {
            _type = type;
            _regexp = regex;
            _tokenList = tokens;
        }

        internal string GetRegex()
        {
            return _regexp;
        }

        internal GDL_Type GetItemType()
        {
            return _type;
        }

        internal List<GDL_Type> GetTokenList()
        {
            return _tokenList;
        }
    }
}
