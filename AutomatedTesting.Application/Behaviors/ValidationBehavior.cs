using AutomatedTesting.Shared;
using FluentValidation;
using MediatR;

namespace AutomatedTesting.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct)));
        var failures = results.SelectMany(r => r.Errors).Where(f => f is not null).ToList();

        if (failures.Count == 0)
            return await next();

        var message = string.Join(" ", failures.Select(f => f.ErrorMessage));

        // Every handler returns Result<T> — build Result<T>.ValidationError(message)
        var responseType = typeof(TResponse);
        if (responseType.IsGenericType &&
            responseType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var innerType = responseType.GetGenericArguments()[0];
            var factory = responseType.GetMethod(
                nameof(Result<object>.ValidationError),
                new[] { typeof(string), innerType });

            var data = innerType.IsValueType ? Activator.CreateInstance(innerType) : null;
            return (TResponse)factory!.Invoke(null, new[] { message, data })!;
        }

        // Fallback for any handler not using Result<T>
        throw new ValidationException(failures);
    }
}