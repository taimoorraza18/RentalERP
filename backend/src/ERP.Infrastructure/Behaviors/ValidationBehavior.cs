using ERP.SharedKernel.Results;
using FluentValidation;
using MediatR;
using System.Reflection;
using DomainExceptions = ERP.SharedKernel.Exceptions;

namespace ERP.Infrastructure.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count == 0)
            return await next();

        var validationError = Error.Validation(
            failures[0].PropertyName,
            string.Join("; ", failures.Select(f => f.ErrorMessage)));

        if (typeof(TResponse) == typeof(Result))
            return (TResponse)(object)Result.Failure(validationError);

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var valueType = typeof(TResponse).GetGenericArguments()[0];
            var method = typeof(Result<>)
                .MakeGenericType(valueType)
                .GetMethod(
                    nameof(Result<object>.Failure),
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    [typeof(Error)],
                    null)!;
            return (TResponse)method.Invoke(null, [validationError])!;
        }

        throw new DomainExceptions.ValidationException(failures.Select(f => f.ErrorMessage));
    }
}
