using Application.FormRequests.Commands;
using Application.FormRequests.Dtos;
using Application.FormRequests.Queries;
using Kedy.Result;
using Kedy.Result.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Kedy.Result.IResult;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormController(ISender sender) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<IDataResponse<Guid>>> Create([FromBody] CreateUpdateFormDto dto)
        => Ok(await sender.Send(new CreateFormCommand(dto)));

    [HttpPut("{id}")]
    public async Task<ActionResult<IResponse>> Update([FromRoute] Guid id, [FromBody] CreateUpdateFormDto dto)
        => Ok(await sender.Send(new UpdateFormCommand(id, dto)));

    [HttpDelete("{id}")]
    public async Task<ActionResult<IResponse>> Delete([FromRoute] Guid id)
        => Ok(await sender.Send(new DeleteFormCommand(id)));

    [HttpGet("{id}")]
    public async Task<ActionResult<IDataResponse<FormDto>>> Get([FromRoute] Guid id)
        => Ok(await sender.Send(new GetFormQuery(id)));

    [HttpGet]
    public async Task<ActionResult<IListResponse<FormDto>>> List()
        => Ok(await sender.Send(new ListFormsQuery()));
}