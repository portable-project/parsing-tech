using System;

namespace marpa_impl
{
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
        public ErrorDescription(ErrorCode _code)
        {
            errorCode = _code;
            description = ErrorHandler.GetErrorMessageByCode(_code);
            item = null;
        }
    }
}
