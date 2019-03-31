using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NSubstitute;
using Serilog;
using System;
using System.Collections.Generic;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Api.Filters;
using Waes.Assignment.Application.Exceptions;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Api.Filters
{
    public class ExceptionsFilterTests
    {
        private readonly ExceptionsFilter _sut;

        public ExceptionsFilterTests()
        {
            _sut = new ExceptionsFilter(Substitute.For<ILogger>());
        }

        [Theory, AutoNSubstituteData]
        public void OnException_WhenEntityAlreadyExistsException_ShouldReturnsConflict(ExceptionContext exceptionContext, EntityAlreadyExistsException entityEx)
        {
            exceptionContext.Exception = entityEx;

            _sut.OnException(exceptionContext);

            exceptionContext.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status409Conflict);
            exceptionContext.Result.Should().BeEquivalentTo(new JsonResult(
                new ErrorResponse
                {
                    Errors = new List<Error>
                    {
                        new Error(ApiCodes.EntityAlreadyExists, entityEx.Message)
                    }
                }));
        }

        [Theory, AutoNSubstituteData]
        public void OnException_WhenOtherException_ShouldReturnsInternalServerError(ExceptionContext exceptionContext, Exception ex)
        {
            exceptionContext.Exception = ex;

            _sut.OnException(exceptionContext);

            exceptionContext.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            exceptionContext.Result.Should().BeEquivalentTo(new JsonResult(new ErrorResponse
            {
                Errors = new List<Error>
                {
                    new Error(ApiCodes.OperationFailure, "An error occurred during the operation.")
                }
            }));
        }
    }
}
