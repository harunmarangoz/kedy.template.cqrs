using Application.FormRequests.Dtos;
using Kedy.Result;
using MediatR;

namespace Application.FormRequests.Commands;

public record CreateFormCommand(CreateUpdateFormDto Dto) : IRequest<IDataResult<Guid>>;