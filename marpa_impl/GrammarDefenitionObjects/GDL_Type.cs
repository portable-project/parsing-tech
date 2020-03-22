using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    public enum GDL_Type
    {
        NAMESPACE,
        RULE_SET,
        IMPORT,
        RULE,

        ATTRIBUTES,
        EXPRESSION,

        NAME,
        NUMBER,
        HEX,
        COMPLEX_NAME
    }
}
