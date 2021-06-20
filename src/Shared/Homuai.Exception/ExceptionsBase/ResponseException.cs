using System;

namespace Homuai.Exception.ExceptionsBase
{
    public class ResponseException : SystemException
    {
        public string Token { get; set; }
        public object Exception { get; set; }
    }
}
