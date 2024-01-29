using Application.FormRequests.Commands;
using FluentValidation;

namespace Infrastructure.FormRequestHandlers.Validators;

public class CreateFormCommandValidator : AbstractValidator<CreateFormCommand>
{
    public CreateFormCommandValidator()
    {
        RuleFor(x => x.Dto).SetValidator(new CreateUpdateFormDtoValidator());
    }
}