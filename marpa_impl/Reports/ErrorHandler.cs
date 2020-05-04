using System;
using System.Collections.Generic;

namespace marpa_impl
{
    internal class ErrorHandler
    {
        private List<ErrorDescription> Reports = new List<ErrorDescription>();
        internal void AddNewError(ErrorCode errorCode, Object obj)
        {
            Reports.Add(new ErrorDescription(errorCode, obj));
        }
        internal List<ErrorDescription> GetErrorDescriptionList()
        {
            return Reports;
        }
        internal static String GetErrorMessageByCode(ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.NO_START_SYMBOL_DETECTED:
                    return "No start symbol for the grammar was provided";
                case ErrorCode.NO_START_SYMBOL_IN_LHS_OF_RULES:
                    return "Start symbol not presented in grammar rule list on right hand side of the rule";
                case ErrorCode.NULL_STRING_SYMBOL_ON_LHS:
                    return "Null string symbol appeared on left hand side of the rule";
                case ErrorCode.NULL_STRING_SYMBOL_NOT_ALONE_ON_RHS:
                    return "Null string symbol appeared in right hand side of the rule with other symbols";
                case ErrorCode.RHS_HAS_NO_SYMBOLS:
                    return "No symbols on right hand side of the rule";
                case ErrorCode.INCORRECT_GRAMMAR:
                    return "Grammar is not valid. Try to precompute it for more detailed information";
                case ErrorCode.NO_GRAMMAR:
                    return "No grammar object was provided";
                case ErrorCode.NO_ERROR:
                    return "No error was found!";
                default: return "Unrecognised Error";
            }
        }
    }
}
