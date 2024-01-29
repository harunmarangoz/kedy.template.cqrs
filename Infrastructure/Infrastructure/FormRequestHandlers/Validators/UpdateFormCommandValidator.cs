using Application.FormRequests.Commands;
using FluentValidation;

namespace Infrastructure.FormRequestHandlers.Validators;

public class UpdateFormCommandValidator : AbstractValidator<UpdateFormCommand>
{
    public UpdateFormCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Dto).SetValidator(new CreateUpdateFormDtoValidator());
    }
}