using AutoMapper;
using SampleMVCWithCQS2.Models;
using SampleMVCWithCQS2.Application.Queries;
using SampleMVCWithCQS2.Application.Commands;
using SampleMVCWithCQS2Core.Domain;

namespace SampleMVCWithCQS2.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SampleMVCWithCQS2.Application.Queries.Product, SaveProductCommand>();
            CreateMap<SaveProductCommand, SampleMVCWithCQS2Core.Domain.Product>();
            CreateMap<SampleMVCWithCQS2.Application.Queries.Product, CreateProductCommand>();
            CreateMap<CreateProductCommand, SampleMVCWithCQS2Core.Domain.Product>();
            CreateMap<SampleMVCWithCQS2.Application.Queries.Product, DeleteProductCommand>();
        }
    }
}