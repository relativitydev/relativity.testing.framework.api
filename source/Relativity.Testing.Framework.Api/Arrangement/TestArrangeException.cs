using System;
using System.Runtime.Serialization;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Represents error of failed test arrangement.
	/// </summary>
	[Serializable]
	public class TestArrangeException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TestArrangeException"/> class.
		/// </summary>
		public TestArrangeException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TestArrangeException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public TestArrangeException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TestArrangeException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public TestArrangeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TestArrangeException"/> class.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
		protected TestArrangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
