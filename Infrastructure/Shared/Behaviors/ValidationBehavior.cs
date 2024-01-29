using Domain.Exceptions;
using Kedy.Result;
using FluentValidation;
using MediatR;
using Shared.Extensions;

namespace Shared.Behaviors;

public class ValidationBehavior<TRequest, TResult>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResult>
    where TRequest : class, IRequest<TResult>
    where TResult : IResult
{
    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        if (validators == null || !validators.Any()) return await next.Invoke();

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)
            ));

        if (validationResults.All(x => x.IsValid)) return await next.Invoke();

        var errors = new List<KeyValuePair<string, string>>();
        foreach (var validationResult in validationResults)
        {
            errors.AddRange(validationResult.Errors.Select(x =>
                new KeyValuePair<string, string>(x.PropertyName, $"{x.ErrorMessage}")));
        }

        throw new AppValidationException(errors);
    }
}