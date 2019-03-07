using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using Waes.Assignment.Api.Filters;
using Waes.Assignment.Api.UnitTests.AutoData;
using Waes.Assignment.Domain.Exceptions;
using Waes.Assignment.Infrastructure.MongoDBStorage.Exceptions;
using Xunit;

namespace Waes.Assignment.Api.UnitTests.Filters
{
    public class ExceptionsFilterTests
    {        
        [Theory, AutoNSubstituteData]
        public void OnException_WhenContextHasException_ShouldSetContextAsExpected(Exception ex,
            ExceptionsFilter sut, ExceptionContext context)
        {
            context.Exception = ex;

            sut.OnException(context);

            (context.Result as JsonResult).StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            (context.Result as JsonResult).Value.Should().BeEquivalentTo(new { Message = "Unexpected error" });
        }

        [Theory, AutoNSubstituteData]
        public void OnException_WhenContextHasDataNotFoundException_ShouldSetContextAsExpected(DataNotFoundException ex,
            ExceptionsFilter sut, ExceptionContext context)
        {
            context.Exception = ex;

            sut.OnException(context);

            (context.Result as JsonResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            (context.Result as JsonResult).Value.Should().BeEquivalentTo(new { ex.Message });
        }

        [Theory, AutoNSubstituteData]
        public void OnException_WhenContextHaCorrelationIdAlreadyUsedForDataException_ShouldSetContextAsExpected(CorrelationIdAlreadyUsedForDataException ex,
            ExceptionsFilter sut, ExceptionContext context)
        {
            context.Exception = ex;

            sut.OnException(context);

            (context.Result as JsonResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (context.Result as JsonResult).Value.Should().BeEquivalentTo(new { ex.Message });
        }
    }
}
