using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RMS.Application.Common.Responses;

namespace RMS.API.Filters;

public class ValidationFilter<TRequest> : IAsyncActionFilter
{
    private readonly IValidator<TRequest> _validator;

    public ValidationFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var request = context.ActionArguments
            .Values
            .OfType<TRequest>()
            .FirstOrDefault();

        if (request == null)
        {
            await next();
            return;
        }

        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(x => x.ErrorMessage)
                .ToList();

            context.Result = new BadRequestObjectResult(
                ApiResponse<object>.Fail("Validation failed.",errors));

            return;
        }

        await next();
    }
}