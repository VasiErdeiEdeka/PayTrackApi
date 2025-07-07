using Microsoft.AspNetCore.Mvc;
using PayTrack.Application.Dtos;
using PayTrack.Application.Interfaces;

namespace PayTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(ITransactionService service) : ControllerBase
{
    /// <summary>
    /// Gets all active transactions.
    /// </summary>
    /// <returns>List of transactions.</returns>
    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Gets a transaction by ID.
    /// </summary>
    /// <param name="id">The transaction ID.</param>
    /// <returns>Transaction data.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await service.GetByIdAsync(id));

    /// <summary>
    /// Gets all transactions for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>List of transactions for the user.</returns>
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId) => Ok(await service.GetByUserIdAsync(userId));

    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    /// <param name="dto">Transaction data to create.</param>
    /// <returns>The created transaction.</returns>
    [HttpPost]
    public async Task<IActionResult> Post(CreateTransactionDto dto) => Ok(await service.CreateAsync(dto));

    /// <summary>
    /// Gets total transaction amount per user.
    /// </summary>
    /// <returns>A dictionary of userId and total amount.</returns>
    [HttpGet("summary/by-user")]
    public async Task<IActionResult> TotalByUser() => Ok(await service.GetTotalAmountPerUserAsync());

    /// <summary>
    /// Gets total transaction amount per transaction type.
    /// </summary>
    /// <returns>A dictionary of transaction type and total amount.</returns>
    [HttpGet("summary/by-type")]
    public async Task<IActionResult> TotalByType() => Ok(await service.GetTotalAmountPerTypeAsync());

    /// <summary>
    /// Gets transactions above a certain threshold.
    /// </summary>
    /// <param name="threshold">The threshold amount.</param>
    /// <returns>List of transactions where amount exceeds the threshold.</returns>
    [HttpGet("summary/above/{threshold}")]
    public async Task<IActionResult> HighVolume(decimal threshold) => Ok(await service.GetAboveThresholdAsync(threshold));
}
