using System;
using System.Runtime.Serialization;

namespace Relativity.Testing.Framework.Api.Exceptions
{
    /// <summary>
    /// The Exception that is thrown when there is a FatalException on a JobReport.
    /// </summary>
    [Serializable]
    public class JobReportException : Exception
    {
        internal JobReportException(string message)
            : base(message)
        {
        }

        internal JobReportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal JobReportException()
        {
        }

        protected JobReportException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
