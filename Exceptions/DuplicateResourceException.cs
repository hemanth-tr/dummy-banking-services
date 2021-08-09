using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingServices.Exceptions
{
	/// <summary>
	/// Exception thrown when there is a same resource exists.
	/// </summary>
	public class DuplicateResourceException : Exception
	{
		/// <summary>
		/// Instantiates exception.
		/// </summary>
		public DuplicateResourceException() { }

		/// <summary>
		/// Instatiates <see cref="DuplicateResourceException"/>
		/// </summary>
		/// <param name="errorMessage"></param>
		public DuplicateResourceException(string errorMessage) : base(errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		/// <summary>
		/// Gets or sets error message for an exception.
		/// </summary>
		public string ErrorMessage { get; set; }
	}
}
