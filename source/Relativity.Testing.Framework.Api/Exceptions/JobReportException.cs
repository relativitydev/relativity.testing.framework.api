using System;
using System.Runtime.Serialization;

namespace Relativity.Testing.Framework.Api.Exceptions
{
    [Serializable]
    public class JobReportException : Exception
    {
        public JobReportException(string message)
            : base(message)
        {
        }

        public JobReportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public JobReportException()
        {
        }

        protected JobReportException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
