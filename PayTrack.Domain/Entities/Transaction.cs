using PayTrack.Domain.Common;
using PayTrack.Domain.Enums;

namespace PayTrack.Domain.Entities;

public class Transaction : BaseEntity
{
    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public User? User { get; set; }

    public TransactionType TransactionType { get; set; }
}