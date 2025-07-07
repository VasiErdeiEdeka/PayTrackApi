using AutoMapper;
using PayTrack.Application;
using PayTrack.Application.Dtos;
using PayTrack.Application.Interfaces;
using PayTrack.Domain.Entities;
using PayTrack.Infrastructure.Repositories;

namespace PayTrack.Infrastructure.Services;

public class TransactionService(
    IRepository<Transaction> transactionRepository,
    IRepository<User> userRepository,
    IMapper mapper)
    : ITransactionService
{
    public async Task<List<TransactionDto>> GetAllAsync()
    {
        var transactions = await transactionRepository.GetAllAsync(x => x.IsActive);
        return mapper.Map<List<TransactionDto>>(transactions);
    }

    public async Task<TransactionDto> GetByIdAsync(int id)
    {
        var transaction = await transactionRepository.GetByIdAsync(id);
        return mapper.Map<TransactionDto>(transaction.IsActive ? transaction : null);
    }

    public async Task<TransactionDto> CreateAsync(CreateTransactionDto dto)
    {
        var transaction = mapper.Map<Transaction>(dto);
        //Validate user 
        var existingUser = await userRepository.GetByIdAsync(dto.UserId);
        if (existingUser is null || !existingUser.IsActive)
        {
            throw new CustomException("User not found.");
        }

        await transactionRepository.AddAsync(transaction);
        await transactionRepository.SaveChangesAsync();

        return mapper.Map<TransactionDto>(transaction);
    }

    public async Task<List<TransactionDto>> GetByUserIdAsync(int userId)
    {
        //Validate user 
        var existingUser = await userRepository.GetByIdAsync(userId);
        if (existingUser is null || !existingUser.IsActive)
        {
            throw new CustomException("User not found.");
        }

        var all = await transactionRepository.GetAllAsync();
        var userTxns = all.Where(t => t.UserId == userId).ToList();
        return mapper.Map<List<TransactionDto>>(userTxns);
    }

    public async Task<Dictionary<int, decimal>> GetTotalAmountPerUserAsync()
    {
        var all = await transactionRepository.GetAllAsync();
        return all
            .GroupBy(t => t.UserId)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
    }

    public async Task<Dictionary<string, decimal>> GetTotalAmountPerTypeAsync()
    {
        var all = await transactionRepository.GetAllAsync();
        return all
            .GroupBy(t => t.TransactionType.ToString())
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
    }

    public async Task<List<TransactionDto>> GetAboveThresholdAsync(decimal threshold)
    {
        var all = await transactionRepository.GetAllAsync();
        var highTxns = all.Where(t => t.Amount > threshold).ToList();
        return mapper.Map<List<TransactionDto>>(highTxns);
    }
}