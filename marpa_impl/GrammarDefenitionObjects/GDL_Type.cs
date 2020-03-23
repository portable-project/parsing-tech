using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    public enum GDL_Type
    {
        DEFENITION,
        RULE_SET,
        IMPORT,
        RULE_SET_IMPORT,
        RULES,
        RULE,
        ALIAS,

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
