﻿using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System.Text;
using Waes.Diff.Core.Models;
using Waes.Diff.Core.UnitTests.AutoData;
using Waes.Diff.Core.UnitTests.Helpers;
using Xunit;

namespace Waes.Diff.Core.UnitTests
{
    public class SizeCheckerTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(SizeChecker).GetConstructors());
        }

        [Theory]
        [InlineNSubstituteData("abc123", "abc123", true)]
        [InlineNSubstituteData("abc123", "XYZ987", true)]
        [InlineNSubstituteData("   ", "   ", true)]
        [InlineNSubstituteData("abc1234", "abc123", false)]
        [InlineNSubstituteData("    ", "  ", false)]
        public void Check_WhenDataProvided_ShouldReturnResultAsExpected(string leftContent, string rightContent, bool resultExpected, SizeChecker sut)
        {            
            var leftData =  DataHelper.CreateData(Encoding.UTF8.GetBytes(leftContent), leftContent.Length, SideEnum.Left);
            var rightData = DataHelper.CreateData(Encoding.UTF8.GetBytes(rightContent), rightContent.Length, SideEnum.Right);

            sut.DiffChecker.Check(leftData, rightData).Returns(new DiffResult());

            var result = sut.Check(leftData, rightData);

            result.SameSize.Should().Be(resultExpected);
        }        
    }
}
