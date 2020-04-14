using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;

using FluentValidation.Internal;
using SampleMVCWithCQS2.Application.Commands;
using SampleMVCWithCQS2.Application.Queries;
using SampleMVCWithCQS2Core.DataAccess;
using SampleMVCWithCQS2Core.Domain;
namespace SampleMVCWithCQS2.Application.Validators
{
    public class ProductValidator : AbstractValidator<SampleMVCWithCQS2.Application.Queries.Product>
    {
        private IProductRepository _productRepository;

        public ProductValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.Name).NotEmpty().Must((p, name) =>
            {
                var isNameExisted = _productRepository.IsNameExisted(name);
                if (!isNameExisted || (!p.IsNew && _productRepository.GetAsync(p.Id).Result.Name == p.Name))
                {
                    return true;
                }
                else
                {
                    return !isNameExisted;

                }
            }).WithMessage("{PropertyName} must must be unique");
            RuleFor(p => p.Category).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Color).NotEmpty().ValueMustBeInEnum<SampleMVCWithCQS2.Application.Queries.Product, string>(typeof(Colors));
        }
    }

    public static class MyCustomValidators
    {
        public static IRuleBuilderOptions<T, string> ValueMustBeInEnum<T, TElement>(this IRuleBuilder<T, string> ruleBuilder, Type type)
        {
            return ruleBuilder.Must((rootObject, value, context) =>
            {
                context.MessageFormatter
                    .AppendArgument("AcceptedValues", string.Join(", ", Enum.GetNames(type).ToList()));
                return value == null || Enum.IsDefined(type, value);
            })
            .WithMessage("{PropertyName} must be only in {AcceptedValues} items.");
        }

    }
}