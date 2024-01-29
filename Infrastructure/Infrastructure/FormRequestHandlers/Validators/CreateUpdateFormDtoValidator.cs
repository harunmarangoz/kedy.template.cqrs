using Application.FormRequests.Dtos;
using FluentValidation;

namespace Infrastructure.FormRequestHandlers.Validators;

public class CreateUpdateFormDtoValidator : AbstractValidator<CreateUpdateFormDto>
{
    public CreateUpdateFormDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}