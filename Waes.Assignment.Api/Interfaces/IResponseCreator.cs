using Microsoft.AspNetCore.Mvc;

namespace Waes.Assignment.Api.Interfaces
{
    /// <summary>
    /// IResponseCreator interface is used to handle the response of any controller
    /// </summary>
    public interface IResponseCreator
    {
        /// <summary>
        /// Creates a created response
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        IActionResult ResponseCreated(object result);

        /// <summary>
        /// Creates an ok response
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        IActionResult ResponseOK(object result);

        /// <summary>
        /// Creates a not found response
        /// </summary>
        /// <returns></returns>
        IActionResult ResponseNotFound();

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <returns></returns>
        IActionResult ResponseError();
    }
}
