using Application.FormRequests.Dtos;
using Application.FormRequests.Queries;
using Kedy.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Interfaces;

namespace Infrastructure.FormRequestHandlers.Queries;

public class GetFormQueryHandler(DatabaseContext databaseContext) : IRequestHandler<GetFormQuery, IDataResult<FormDto>>
{
    public async Task<IDataResult<FormDto>> Handle(GetFormQuery request, CancellationToken cancellationToken)
    {
        var form = await databaseContext.Forms.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var formDto = form.Adapt<FormDto>();

        return new SuccessDataResult<FormDto>(formDto);
    }
}