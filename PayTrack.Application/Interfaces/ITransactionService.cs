using PayTrack.Application.Dtos;

namespace PayTrack.Application.Interfaces;

public interface ITransactionService
{
    Task<List<TransactionDto>> GetAllAsync();

    Task<TransactionDto> GetByIdAsync(int id);

    Task<TransactionDto> CreateAsync(CreateTransactionDto dto);

    Task<List<TransactionDto>> GetByUserIdAsync(int userId);


    Task<Dictionary<int, decimal>> GetTotalAmountPerUserAsync();

    Task<Dictionary<string, decimal>> GetTotalAmountPerTypeAsync();

    Task<List<TransactionDto>> GetAboveThresholdAsync(decimal threshold);
}