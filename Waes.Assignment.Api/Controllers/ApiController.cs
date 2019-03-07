using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Waes.Assignment.Application.Notifications;
using Waes.Assignment.Application.Notifications.Enums;
using Waes.Assignment.Application.Notifications.Interfaces;

namespace Waes.Assignment.Api.Controllers
{
    public class ApiController : ControllerBase
    {
        public IGetNotifications<WarningNotification> WarningNotifications { get; }

        public ApiController(IGetNotifications<WarningNotification> notificationHandler)
        {
            WarningNotifications = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        }

        protected new IActionResult ResponseOK(object result = null)
        {            
            if (WarningNotifications.Get().Any())
            {
                return ClientError();
            }

            return Ok(new ResponseWrapper
            {
                Success = true,
                Data = result
            });
        }

        protected new IActionResult ResponseCreated(string actionName, object result)
        {
            if (WarningNotifications.Get().Any())
            {
                return ClientError();
            }

            return Created(actionName, new ResponseWrapper
            {
                Success = true,
                Data = result
            });
        }

        private IActionResult ClientError()
        {
            var response = new ResponseWrapper
            {
                Success = false,
                Errors = WarningNotifications.Get().Select(n => n.Value)
            };

            if (WarningNotifications.Get().All(x => x.Type == NotificationType.ResourceDuplicated))
            {
                return Conflict(response);
            }
            else if (WarningNotifications.Get().All(x => x.Type == NotificationType.NotFound))
            {
                return NotFound(response);
            }

            return BadRequest(response);
        }
    }
}
