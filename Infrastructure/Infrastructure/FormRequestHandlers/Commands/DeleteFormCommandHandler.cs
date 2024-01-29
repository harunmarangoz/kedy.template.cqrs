using Application.FormRequests.Commands;
using Kedy.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Interfaces;

namespace Infrastructure.FormRequestHandlers.Commands;

public class DeleteFormCommandHandler(DatabaseContext databaseContext)
    : IRequestHandler<DeleteFormCommand, IResult>
{
    public async Task<IResult> Handle(DeleteFormCommand request, CancellationToken cancellationToken)
    {
        var form = await databaseContext.Forms.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (form == null) return new ErrorResult("Form not found");

        databaseContext.Forms.Remove(form);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return new SuccessResult();
    }
}