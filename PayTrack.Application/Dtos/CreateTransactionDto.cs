using System.ComponentModel.DataAnnotations;
using PayTrack.Domain.Enums;

namespace PayTrack.Application.Dtos;

public class CreateTransactionDto(decimal amount)
{
    public int UserId { get; set; }

    public decimal Amount { get; set; } = amount;

    [Range(0, 1, ErrorMessage = "TransactionType must be Debit (0) or Credit (1).")]

    public TransactionType TransactionType { get; set; }
}