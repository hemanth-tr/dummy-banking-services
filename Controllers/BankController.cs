using BankingServices.Exceptions;
using BankingServices.Model;
using BankingServices.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BankingServices.Controllers
{
	/// <summary>
	/// Controller for bank.
	/// </summary>
	[ApiController]
	[Route("api/banks")]
	public class BankController : ControllerBase
	{
		/// <summary>
		/// Bank controller constructor.
		/// </summary>
		/// <param name="logger">Logger instance.</param>
		/// <param name="bankRepository">Bank repository instance.</param>
		public BankController(ILogger<BankController> logger, IBankRepository bankRepository)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			BankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
		}

		/// <summary>
		/// Gets or sets the instance of <see cref="ILogger"/>.
		/// </summary>
		private ILogger<BankController> Logger { get; init; }

		/// <summary>
		/// Gets or sets the instance of <see cref="IBankRepository"/>
		/// </summary>
		private IBankRepository BankRepository { get; init; }

		/// <summary>
		/// Retrieves the list of banks.
		/// </summary>
		/// <returns>List of banks.</returns>
		[HttpGet(Name = "FetchBanks")]
		public async Task<IActionResult> FetchBanksAsync()
		{
			try
			{
				var result = await BankRepository.FetchBanksAsync().ConfigureAwait(false);

				if (result == null || result.Count < 1)
				{
					return NoContent();
				}

				return Ok(result);
			}
			catch (Exception ex)
			{
				Logger.LogError($"Exception fetching banks. ExceptionMessage: {ex.Message}");
				return StatusCode(500, "Internel server error");
			}
		}

		/// <summary>
		/// Retrieves bank details for the provided identifier.
		/// </summary>
		/// <param name="id">Unique identifier.</param>
		/// <returns>Bank details.</returns>
		[HttpGet("{id}", Name = "FetchBank")]
		public async Task<IActionResult> GetBankAsync(Guid id)
		{
			if (id == Guid.Empty)
			{
				return BadRequest();
			}

			var result = await BankRepository.FetchBankAsync((x) => x.Id == id).ConfigureAwait(false);
			if (result == null)
			{
				return NotFound(id);
			}

			return Ok(result);
		}

		/// <summary>
		/// Creates a new bank.
		/// </summary>
		/// <param name="bank">Details required to create bank.</param>
		/// <returns>Unique identifier for bank.</returns>
		[HttpPost(Name = "CreateBank")]
		public async Task<IActionResult> CreateBankAsync([FromBody] Bank bank)
		{
			if (bank == null)
			{
				return BadRequest(bank);
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(bank);
			}

			Guid result = Guid.Empty;
			try
			{
				result = await BankRepository.CreateBankAsync(bank).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				Logger.LogError($"{ex.Message}");

				if (ex is DuplicateResourceException dEx)
				{
					return Conflict(dEx.Message);
				}
				else if (ex is ResourceCreationFailedException rcfEx)
				{
					ushort httpStatusCode = (ushort)HttpStatusCode.ServiceUnavailable;
					return StatusCode(httpStatusCode, rcfEx.Message);
				}
			}

			return CreatedAtRoute("FetchBank", new { id = result }, null);
		}

		/// <summary>
		/// Updates status for provided bank's id.
		/// </summary>
		/// <param name="id">bank's id.</param>
		/// <param name="status">status to be updated <see cref="Status"/>.</param>
		/// <returns></returns>
		[HttpPut("{id}", Name = "UpdateBankStatus")]
		public async Task<IActionResult> UpdateBankStatusAsync(Guid id, [FromBody] string status)
		{
			if (id == Guid.Empty)
			{
				return BadRequest(id);
			}

			if (!Enum.TryParse(status, out Status bankStatus))
			{
				return BadRequest(status);
			}

			try
			{
				var result = await BankRepository.FetchBankAsync((x) => x.Id == id).ConfigureAwait(false);
				if (result == null)
				{
					return NotFound(id);
				}

				await BankRepository.ChangeBankStatusAsync(result.Id, bankStatus).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				Logger.LogError(ex.Message);
				return StatusCode(500, ex.Message);
			}

			return Ok();
		}
	}
}
