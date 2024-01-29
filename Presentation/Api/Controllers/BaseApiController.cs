using Kedy.Result;
using Kedy.Result.Response;
using Microsoft.AspNetCore.Mvc;
using IResult = Kedy.Result.IResult;

namespace Api.Controllers;

[ApiController]
public class BaseApiController : ControllerBase
{
    protected ActionResult Ok(IResult result)
    {
        var response = new Response(result);
        if (!result.HasError) return base.Ok(response);
        return BadRequest(response);
    }

    protected ActionResult Ok<T>(IDataResult<T> result)
    {
        var response = new DataResponse<T>(result);
        if (!result.HasError) return base.Ok(response);
        return BadRequest(response);
    }

    protected ActionResult Ok<T>(IListResult<T> result)
    {
        var response = new ListResponse<T>(result);
        if (!result.HasError) return base.Ok(response);
        return BadRequest(response);
    }

    protected ActionResult Ok<T>(IPagedResult<T> result)
    {
        var response = new PagedResponse<T>(result);
        if (!result.HasError) return base.Ok(response);
        return BadRequest(response);
    }
}