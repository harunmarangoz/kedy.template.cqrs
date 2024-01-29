using Application.FormRequests.Commands;
using Kedy.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Interfaces;

namespace Infrastructure.FormRequestHandlers.Commands;

public class UpdateFormCommandHandler(DatabaseContext databaseContext)
    : IRequestHandler<UpdateFormCommand, IResult>
{
    public async Task<IResult> Handle(UpdateFormCommand request, CancellationToken cancellationToken)
    {
        var form = await databaseContext.Forms.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (form == null) return new ErrorResult("Form not found");

        form.Name = request.Dto.Name;
        databaseContext.Forms.Update(form);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return new SuccessResult();
    }
}