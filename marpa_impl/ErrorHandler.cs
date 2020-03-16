using System;

namespace marpa_impl
{
    public enum ErrorCode
    {
        NO_ERROR,
        NO_SUCH_SYMBOL_IN_GRAMMAR,
        INCORRECT_RULE_SYMBOLS,
        INCOMPLETE_GRAMMAR,
        NULLING_SYMBOL_BELONGS_TO_GRAMMAR
    }

    public static class ErrorHandler
    {
        public static String getErrorMessageByCode(ErrorCode errorCode)
        {
            return errorCode.ToString();
        }
        public static void PrintErrorCode(ErrorCode error)
        {
            Console.WriteLine("ATTENTION: " + getErrorMessageByCode(error));
        }
        public static void PrintErrorCode(ErrorCode error, Symbol symbol)
        {
            Console.WriteLine("ATTENTION: " + getErrorMessageByCode(error) + " : " + symbol.GetSymbolName());
        }
        public static void PrintErrorCode(ErrorCode error, Rule rule)
        {
            Console.WriteLine("ATTENTION: " + getErrorMessageByCode(error) + " : " + rule.ToString());
        }
    }
}
