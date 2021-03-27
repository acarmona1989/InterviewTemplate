using FluentValidation.Results;
using Football.Application.Commons.Exceptions;
using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using System;

namespace Football.Application.UnitTests.Common.Exceptions
{
    public class ValidationExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Test]
        public void SingleValidationFailureCreatesASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("AmoutAwayPlayer", "Players amount must be higher than 1")
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "AmoutAwayPlayer" });
            actual["AmoutAwayPlayer"].Should().BeEquivalentTo(new string[] { "Players amount must be higher than 1" });
        }
    }
}
