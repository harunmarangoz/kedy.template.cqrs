using Application.FormRequests.Dtos;
using Application.FormRequests.Queries;
using Kedy.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Interfaces;

namespace Infrastructure.FormRequestHandlers.Queries;

public class ListFormsQueryHandler(DatabaseContext databaseContext)
    : IRequestHandler<ListFormsQuery, IListResult<FormDto>>
{
    public async Task<IListResult<FormDto>> Handle(ListFormsQuery request, CancellationToken cancellationToken)
    {
        var forms = await databaseContext.Forms.ToListAsync(cancellationToken);
        var formDtos = forms.Adapt<List<FormDto>>();

        return new SuccessListResult<FormDto>(formDtos);
    }
}