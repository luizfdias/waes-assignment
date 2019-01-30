using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using Waes.Diff.Api.Filters;
using Waes.Diff.Api.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Api.UnitTests.Filters
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
    }
}
