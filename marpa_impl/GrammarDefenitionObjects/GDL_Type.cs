using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl.GrammarDefenitionObjects
{
    internal enum GDL_Type
    {
        DEFAULT,
        DEFENITION,
        RULE_SET,
        IMPORTS,
        RULE_SET_IMPORT,
        RULES,
        RULE,
        ALIAS,

        ATTRIBUTE,
        ATTRIBUTES,
        USAGE,
        USAGE_ARG_LIST,
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
