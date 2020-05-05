using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public enum ErrorCode
    {
        NO_ERROR,

        NO_START_SYMBOL_DETECTED,
        NO_START_SYMBOL_IN_LHS_OF_RULES,
        NULL_STRING_SYMBOL_ON_LHS,
        NULL_STRING_SYMBOL_NOT_ALONE_ON_RHS,
        RHS_HAS_NO_SYMBOLS,

        INCORRECT_GRAMMAR,
        NO_GRAMMAR,
        SYMBOL_POSITION_OUT_OF_RANGE,
        UNRECOGNISED_SYMBOL
    }
}
