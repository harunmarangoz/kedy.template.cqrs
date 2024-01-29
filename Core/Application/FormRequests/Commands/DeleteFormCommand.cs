using Kedy.Result;
using MediatR;

namespace Application.FormRequests.Commands;

public record DeleteFormCommand(Guid Id) : IRequest<IResult>;