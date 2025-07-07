using Moq;
using AutoMapper;
using PayTrack.Application.Dtos;
using PayTrack.Domain.Entities;
using PayTrack.Infrastructure.Services;
using PayTrack.Infrastructure.Repositories;

namespace PayTrack.Tests.Services;

public class TransactionServiceTests
{
    private readonly Mock<IRepository<Transaction>> _mockRepo;
    private readonly TransactionService _service;

    public TransactionServiceTests()
    {
        _mockRepo = new Mock<IRepository<Transaction>>();
        var mockUserRepo = new Mock<IRepository<User>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Transaction, TransactionDto>();
        });
        var mapper = config.CreateMapper();

        _service = new TransactionService(_mockRepo.Object, mockUserRepo.Object, mapper);
    }

    [Fact]
    public async Task GetTotalAmountPerUserAsync_ShouldReturnCorrectTotals()
    {
        // Arrange
        var transactions = new List<Transaction>
        {
            new() { Id = 1, UserId = 1, Amount = 100 },
            new() { Id = 2, UserId = 2, Amount = 50 },
            new() { Id = 3, UserId = 3, Amount = 200 }
        };

        _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Transaction, bool>>>()))
            .ReturnsAsync(transactions);

        // Act
        var result = await _service.GetTotalAmountPerUserAsync();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(100, result[1]);
        Assert.Equal(50, result[2]);
        Assert.Equal(200, result[3]);
    }

    [Fact]
    public async Task GetAboveThresholdAsync_ShouldReturnOnlyHighVolumeTransactions()
    {
        // Arrange
        var transactions = new List<Transaction>
        {
            new() { Id = 1, UserId = 1, Amount = 100 },
            new() { Id = 2, UserId = 1, Amount = 300 },
            new() { Id = 3, UserId = 2, Amount = 500 }
        };

        _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Transaction, bool>>>()))
            .ReturnsAsync(transactions);

        // Act
        var result = await _service.GetAboveThresholdAsync(250);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.True(t.Amount > 250));
        Assert.Contains(result, t => t.Amount == 300);
        Assert.Contains(result, t => t.Amount == 500);
    }
}
