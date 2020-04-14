using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using SampleMVCWithCQS2.Application.Commands;
using SampleMVCWithCQS2.Application.Queries;
using SampleMVCWithCQS2Core.Domain;
using FluentValidation.Internal;
namespace SampleMVCWithCQS2.Application.Validators
{
    public class ProductValidator : AbstractValidator<SampleMVCWithCQS2.Application.Queries.Product>
    {
        public ProductValidator()
        {
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Category).NotEmpty();
            RuleFor(command => command.Price).GreaterThan(0);
            RuleFor(command => command.Color).ValueMustBeInEnum<SampleMVCWithCQS2.Application.Queries.Product, string>(typeof(Colors));
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
                return Enum.IsDefined(type, value);
            })
            .WithMessage("{PropertyName} must be only in {AcceptedValues} items.");
        }
    }
}