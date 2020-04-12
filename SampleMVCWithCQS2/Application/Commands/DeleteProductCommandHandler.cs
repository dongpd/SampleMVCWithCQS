namespace SampleMVCWithCQS2.Application.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using System.Runtime.Serialization;
    using System.Threading;
    using SampleMVCWithCQS2Core.Domain;
    using AutoMapper;

    public class DeleteProductCommandHandler
    : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IMediator mediator, IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { Id = request.Id };
            _productRepository.Remove(product);
            return await _productRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}