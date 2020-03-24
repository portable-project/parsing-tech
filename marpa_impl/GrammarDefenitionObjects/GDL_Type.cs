using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    public enum GDL_Type
    {
        DEFENITION,
        RULE_SET,
        IMPORTS,
        RULE_SET_IMPORT,
        RULES,
        RULE,
        ALIAS,

        ATTRIBUTE,
        ATTRIBUTES,
        EXPRESSION,
        EXPRESSIONS,

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
