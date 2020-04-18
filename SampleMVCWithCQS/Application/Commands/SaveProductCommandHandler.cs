namespace SampleMVCWithCQS.Application.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using System.Runtime.Serialization;
    using System.Threading;
    using SampleMVCWithCQSCore.Domain;
    using AutoMapper;

    public class SaveProductCommandHandler
    : IRequestHandler<SaveProductCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SaveProductCommandHandler(IMediator mediator, IProductRepository productRepository, IMapper mapper)
        {
            _mediator = mediator;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync((int)request.Id);
            _mapper.Map(request, product);
            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}