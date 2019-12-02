using System;
using System.Collections.Generic;
using System.Text;

namespace marpa_impl
{
    public enum ErrorCode
    {
        NO_ERROR,
        NO_SUCH_SYMBOL_IN_GRAMMAR,

    }

    public static class ErrorHandler
    {
        public static String getErrorMessageByCode(ErrorCode errorCode)
        {
            return errorCode.ToString();
        }
    }
}
