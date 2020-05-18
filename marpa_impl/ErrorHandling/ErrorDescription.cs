using System;

namespace marpa_impl
{
    public struct ErrorDescription
    {
        ErrorCode errorCode;
        String description;
        Object item;
        int atPosition;

        internal ErrorDescription(ErrorCode _code, Object _item)
        {
            errorCode = _code;
            description = ErrorHandler.GetErrorMessageByCode(_code);
            item = _item;
            atPosition = -1;
        }
        internal ErrorDescription(ErrorCode _code)
        {
            errorCode = _code;
            description = ErrorHandler.GetErrorMessageByCode(_code);
            item = null;
            atPosition = -1;
        }
        internal ErrorDescription(ErrorCode _code, Object _item, int _position)
        {
            errorCode = _code;
            description = ErrorHandler.GetErrorMessageByCode(_code);
            item = _item;
            atPosition = _position;
        }
    }
}
