using Xunit;
using Numer.Core.Features.RootOfEquation.Commands.BisectionMethod;
using Numer.Domain;
using Numer.Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Numer.Tests
{
    public class BisectionHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnRootResult_WhenFunctionHasRootInInterval()
        {
            // Arrange
            var handler = new BisectionHandler();
            var command = new BisectionCommand
            {
                LowerBound = 1,
                UpperBound = 2,
                Tolerance = 0.0001,
                MaxIterations = 100,
                Function = x => x * x - 2 // Function with root at sqrt(2)
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal((int)EnumMasterType.MasterType.Success, result.Status.StatusCode);
            Assert.InRange(result.Data.Result, 1.4142, 1.4143); // Approximate root is sqrt(2)
            Assert.True(result.Data.Iterations.Count > 0);
            Assert.Equal("Bisection method completed successfully.", result.Status.Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenFunctionDoesNotHaveRootInInterval()
        {
            // Arrange
            var handler = new BisectionHandler();
            var command = new BisectionCommand
            {
                LowerBound = 1,
                UpperBound = 2,
                Tolerance = 0.0001,
                MaxIterations = 100,
                Function = x => x * x + 1
            };

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal((int)EnumMasterType.MasterType.BadRequest, result.Status.StatusCode);
            Assert.Equal("The function must have different signs at the bounds.", result.Status.Message);
        }
    }
}
