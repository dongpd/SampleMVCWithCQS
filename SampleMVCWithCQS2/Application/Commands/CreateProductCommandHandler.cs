namespace SampleMVCWithCQS2.Application.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using System.Runtime.Serialization;
    using System.Threading;
    using SampleMVCWithCQS2Core.Domain;

    public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IMediator mediator, IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Category, request.Price, (Colors)Enum.Parse(typeof(Colors), request.Color), request.InStock);
            _productRepository.Add(product);
            return await _productRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}