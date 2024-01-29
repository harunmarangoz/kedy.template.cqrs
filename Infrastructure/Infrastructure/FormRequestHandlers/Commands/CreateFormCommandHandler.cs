using Application.FormRequests.Commands;
using Kedy.Result;
using Domain.Entities;
using Infrastructure.FormRequestHandlers.Validators;
using Mapster;
using MediatR;
using Persistence.Contexts;
using Shared.Extensions;

namespace Infrastructure.FormRequestHandlers.Commands;

public class CreateFormCommandHandler(DatabaseContext dbContext)
    : IRequestHandler<CreateFormCommand, IDataResult<Guid>>
{
    public async Task<IDataResult<Guid>> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        var form = request.Dto.Adapt<Form>();

        await dbContext.Forms.AddAsync(form, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new SuccessDataResult<Guid>(form.Id);
    }
}