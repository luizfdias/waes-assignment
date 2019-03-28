using Microsoft.AspNetCore.Mvc;

namespace Waes.Assignment.Api.Interfaces
{
    public interface IResponseCreator
    {
        IActionResult ResponseCreated(object result);

        IActionResult ResponseOK(object result);

        IActionResult ResponseNotFound();

        IActionResult ResponseError();
    }
}
