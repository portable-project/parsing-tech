using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    public enum GDL_Type
    {
        DEFENITION,
        RULE_SET,
        RULE_SET_BODY,
        IMPORT,
        RULE,

        ATTRIBUTE,
        ATTRIBUTES,
        EXPRESSION,

        NAME,
        NUMBER,
        HEX,
        COMPLEX_NAME,

        SIMPLE,
        STRING,
        CHARS,
        ANY_CHAR,
        CHAR_CODE,
        GROUP,
        CHECK,
        CHECK_NOT,
        
    }
}
