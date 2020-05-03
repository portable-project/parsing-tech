using System;
using System.Collections.Generic;

namespace marpa_impl
{
    public enum ErrorCode
    {
        NO_START_SYMBOL_DETECTED,
        NO_START_SYMBOL_IN_LHS_OF_RULES,
        NULL_STRING_SYMBOL_ON_LHS,
        NULL_STRING_SYMBOL_NOT_ALONE_ON_RHS,
        RHS_HAS_NO_SYMBOLS,
    }

    
    public struct ErrorDescription
    {
        ErrorCode errorCode;
        String description;
        Object item;

        public ErrorDescription(ErrorCode _code, Object _item)
        {
            errorCode = _code;
            description = ErrorHandler.GetErrorMessageByCode(_code);
            item = _item;
        }
    }

    public struct ErrorReport
    {
        public List<ErrorDescription> errorDescriptions;
        public bool isSuccessfull;
        public int totalErrorCount;

        public ErrorReport(List<ErrorDescription> _errorDescriptions)
        {
            errorDescriptions = _errorDescriptions;
            isSuccessfull = errorDescriptions == null || errorDescriptions.Count == 0;
            totalErrorCount = _errorDescriptions.Count;
        }
    }

    public static class ErrorHandler
    {
        private static List<ErrorDescription> Reports = new List<ErrorDescription>();
        public static void AddNewError(ErrorCode errorCode, Object obj)
        {
            Reports.Add(new ErrorDescription(errorCode, obj));
        }

        public static ErrorReport GetErrorReport()
        {
            return new ErrorReport(Reports);
        }

        public static void FlushErrorList()
        {
            Reports = new List<ErrorDescription>();
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
                default: return "Unrecognised Error";
            }
        }
    }
}
