using Application.FormRequests.Dtos;
using Kedy.Result;
using MediatR;

namespace Application.FormRequests.Queries;

public record ListFormsQuery() : IRequest<IListResult<FormDto>>;