using System;

namespace BankingServices.Exceptions
{
    /// <summary>
    /// Exception thrown when resource is not created.
    /// </summary>
    public class ResourceCreationFailedException : Exception
	{
		/// <summary>
		/// Instantiates <see cref="ResourceCreationFailedException"/>
		/// </summary>
		/// <param name="errorMessage"></param>
		public ResourceCreationFailedException(string errorMessage) : base(errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		/// <summary>
		/// Gets or sets the error message for an exception.
		/// </summary>
		public string ErrorMessage { get; }
	}
}
