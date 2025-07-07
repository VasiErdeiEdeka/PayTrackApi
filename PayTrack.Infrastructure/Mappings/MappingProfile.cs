using AutoMapper;
using PayTrack.Application.Dtos;
using PayTrack.Domain.Entities;


namespace PayTrack.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>();

        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<CreateTransactionDto, Transaction>();

        CreateMap<Transaction, TransactionDto>().ReverseMap();
    }
}