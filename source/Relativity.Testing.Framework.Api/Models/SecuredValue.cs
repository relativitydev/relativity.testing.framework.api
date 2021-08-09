namespace Relativity.Testing.Framework.Api.Models
{
	internal class SecuredValue<T>
	{
		public bool Secured { get; set; }

		public T Value { get; set; }
	}
}
