using Application.FormRequests.Dtos;
using Kedy.Result;
using MediatR;

namespace Application.FormRequests.Commands;

public record UpdateFormCommand(Guid Id, CreateUpdateFormDto Dto) : IRequest<IResult>;