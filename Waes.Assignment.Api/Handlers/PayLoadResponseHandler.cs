using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Api.Handlers
{
    public class PayLoadResponseHandler : IResponseHandler
    {
        private readonly INotificationHandler _notificationHandler;

        public PayLoadResponseHandler(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        }

        public IActionResult ResponseCreated(ControllerBase controller, object result)
        {
            return PayLoadCreated(controller, result) ?? PayLoadAlreadyExist(controller) ?? Error(controller);
        }

        public IActionResult ResponseOK(ControllerBase controller, object result)
        {
            return DiffFound(controller, result) ?? DiffNotFound(controller);
        }

        private IActionResult DiffFound(ControllerBase controller, object result)
        {
            if (result == null)
                return null;

            return controller.Ok(new
            {
                Data = result
            });
        }

        private IActionResult DiffNotFound(ControllerBase controller)
        {
            return controller.NotFound(new
            {
                Errors = new[]
                {
                    new
                    {
                        Code = "900",
                        Message = $"Diff result was not found."
                    }
                }
            });
        }

        private IActionResult PayLoadCreated(ControllerBase controller, object result)
        {
            var payLoadCreatedEvent = _notificationHandler.GetEvent<PayLoadCreatedEvent>();
            var diffAnalyzedEvent = _notificationHandler.GetEvent<DiffAnalyzedEvent>();

            if (payLoadCreatedEvent != null)
                return controller.Created("", new
                {
                    Data = result,
                    Links = diffAnalyzedEvent != null ? new[]
                    {
                        new
                        {
                            Method = "GET",
                            HRef = $"{controller.Request.Scheme}://{controller.Request.Host}{controller.Request.PathBase}/v1/diff/{diffAnalyzedEvent.CorrelationId}",
                            Rel = "self"
                        }
                    } : null
                });

            return null;
        }

        private IActionResult PayLoadAlreadyExist(ControllerBase controller)
        {
            var payLoadAlreadyCreatedEvent = _notificationHandler.GetEvent<PayLoadAlreadyCreatedEvent>();

            if (payLoadAlreadyCreatedEvent != null)
                return controller.Conflict(new
                {
                    Errors = new[]
                    {
                            new
                            {
                                Code = "901",
                                Message = $"Payload with correlation id: {payLoadAlreadyCreatedEvent.CorrelationId} and side: {payLoadAlreadyCreatedEvent.Side} already exist."
                            }
                        }
                });

            return null;
        }

        private IActionResult Error(ControllerBase controller)
        {
            return controller.StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                Errors = new[]
                {
                    new
                    {
                        Code = "999",
                        Message = $"An error occurred during the operation."
                    }
                }
            });
        }
    }
}
