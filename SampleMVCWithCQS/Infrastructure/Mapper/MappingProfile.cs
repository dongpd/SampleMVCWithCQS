using AutoMapper;
using SampleMVCWithCQS.Models;
using SampleMVCWithCQS.Application.Queries;
using SampleMVCWithCQS.Application.Commands;
using SampleMVCWithCQSCore.Domain;

namespace SampleMVCWithCQS.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SampleMVCWithCQS.Application.Queries.Product, SaveProductCommand>();
            CreateMap<SaveProductCommand, SampleMVCWithCQSCore.Domain.Product>();
            CreateMap<SampleMVCWithCQS.Application.Queries.Product, CreateProductCommand>();
            CreateMap<CreateProductCommand, SampleMVCWithCQSCore.Domain.Product>();
            CreateMap<SampleMVCWithCQS.Application.Queries.Product, DeleteProductCommand>();
            CreateMap<UserRegistrationModel, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}