using System;
using System.Runtime.Serialization;

namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    [Serializable]
    internal class PropertyAlreadySetException : Exception
    {
        //public PropertyAlreadySetException()
        //{
        //}

        //public PropertyAlreadySetException(string message) : base(message)
        //{
        //}

        //public PropertyAlreadySetException(string message, Exception innerException) : base(message, innerException)
        //{
        //}

        //protected PropertyAlreadySetException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}

        public PropertyAlreadySetException(string propertyName) : base($"The property {propertyName} is already set -- cannot be overwritten.")
        {        }
    }
}