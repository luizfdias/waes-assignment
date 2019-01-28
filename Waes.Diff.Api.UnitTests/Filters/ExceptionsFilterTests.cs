﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using Waes.Diff.Api.Filters;
using Waes.Diff.Api.UnitTests.AutoData;
using Waes.Diff.Core.Exceptions;
using Xunit;

namespace Waes.Diff.Api.UnitTests.Filters
{
    public class ExceptionsFilterTests
    {
        [Theory, AutoNSubstituteData]
        public void OnException_WhenContextHasBinaryDataNotFoundException_ShouldSetContextAsExpected(BinaryDataNotFoundException ex,
            ExceptionsFilter sut, ExceptionContext context)
        {
            context.Exception = ex;

            sut.OnException(context);

            (context.Result as JsonResult).StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            (context.Result as JsonResult).Value.Should().BeEquivalentTo(new { ex.Message });
        }

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