using System;

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

        protected JobReportException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
