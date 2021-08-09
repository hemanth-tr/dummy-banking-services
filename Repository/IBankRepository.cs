using BankingServices.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingServices.Repository
{
	/// <summary>
	/// Interface for bank repository.
	/// </summary>
	public interface IBankRepository
	{
		/// <summary>
		/// Retrieves the list of banks.
		/// </summary>
		/// <returns></returns>
		Task<IList<BankInformation>> FetchBanksAsync();

		/// <summary>
		/// Retrieves the list of banks.
		/// </summary>
		/// <param name="function">delegate which has logic for information match.</param>
		/// <returns>bank information.</returns>
		Task<BankInformation> FetchBankAsync(Func<BankInformation, bool> function);

		/// <summary>
		/// Creates a bank.
		/// </summary>
		/// <param name="bank"></param>
		/// <returns>unique identifier.</returns>
		Task<Guid> CreateBankAsync(Bank bank);

		/// <summary>
		/// Updates bank's status.
		/// </summary>
		/// <param name="id">bank id.</param>
		/// <param name="status">status to be updated.</param>
		Task ChangeBankStatusAsync(Guid id, Status status);
	}
}
