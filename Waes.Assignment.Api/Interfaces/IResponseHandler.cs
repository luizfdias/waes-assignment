using Microsoft.AspNetCore.Mvc;

namespace Waes.Assignment.Api.Interfaces
{
    public interface IResponseHandler
    {
        IActionResult ResponseCreated(ControllerBase controller, object result);

        IActionResult ResponseOK(ControllerBase controller, object result);
    }
}
