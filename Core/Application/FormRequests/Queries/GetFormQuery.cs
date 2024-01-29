using Application.FormRequests.Dtos;
using Kedy.Result;
using MediatR;

namespace Application.FormRequests.Queries;

public record GetFormQuery(Guid Id) : IRequest<IDataResult<FormDto>>;