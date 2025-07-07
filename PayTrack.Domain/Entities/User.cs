using PayTrack.Domain.Common;

namespace PayTrack.Domain.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }
}