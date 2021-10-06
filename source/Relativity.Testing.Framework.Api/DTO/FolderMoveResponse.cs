namespace Relativity.Testing.Framework.Api.Services
{
	public class FolderMoveResponse
	{
		/// <summary>
		/// Gets or sets the state of the mass process.
		/// </summary>
		public string ProcessState { get; set; }

		/// <summary>
		/// Gets or sets the number of operations needed to be executed to move the object.
		/// </summary>
		public int TotalOperations { get; set; }

		/// <summary>
		/// Gets or sets the number of operations that have been executed.
		/// </summary>
		public int OperationsCompleted { get; set; }
	}
}
