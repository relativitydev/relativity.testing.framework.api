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
		/// <summary>
		/// Initializes a new instance of the <see cref="JobReportException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public JobReportException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JobReportException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public JobReportException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JobReportException"/> class.
		/// </summary>
		public JobReportException()
		{
		}

		protected JobReportException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
