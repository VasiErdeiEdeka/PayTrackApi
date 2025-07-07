using PayTrack.Domain.Enums;

namespace PayTrack.Application.Dtos;

public class TransactionDto
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public decimal Amount { get; set; }

    public TransactionType TransactionType { get; set; }

    public DateTime CreatedAt { get; set; }
}