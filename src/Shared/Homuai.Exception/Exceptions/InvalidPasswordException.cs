﻿using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class InvalidPasswordException : HomuaiException
    {
        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidPasswordException() : base(ResourceTextException.INVALID_PASSWORD)
        {
        }
    }
}
