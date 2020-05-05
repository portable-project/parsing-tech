using System;

namespace marpa_impl
{
    public struct ErrorDescription
    {
        ErrorCode errorCode;
        String description;
        Object item;
        int atPosition;

        public ErrorDescription(ErrorCode _code, Object _item)
        {
            errorCode = _code;
            description = ErrorHandler.GetErrorMessageByCode(_code);
            item = _item;
            atPosition = -1;
        }
        public ErrorDescription(ErrorCode _code)
        {
            errorCode = _code;
            description = ErrorHandler.GetErrorMessageByCode(_code);
            item = null;
            atPosition = -1;
        }
        public ErrorDescription(ErrorCode _code, Object _item, int _position)
        {
            errorCode = _code;
            description = ErrorHandler.GetErrorMessageByCode(_code);
            item = _item;
            atPosition = _position;
        }
    }
}
