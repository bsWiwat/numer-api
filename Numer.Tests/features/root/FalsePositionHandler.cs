// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Xunit;
// using Numer.Core.Features.RootOfEquation.Commands.FalsePositionMethod;


// namespace Numer.Tests.features.root
// {
//     public class FalsePositionHandler
//     {
//         [Fact]
//         public void Handle_ShouldReturnRootResult_WhenFunctionHasRootInInterval()
//         {
//             // Arrange
//             var handler = new FalsePositionHandler();
//             var command = new FalsePositionCommand
//             {
//                 LowerBound = 1,
//                 UpperBound = 2,
//                 Tolerance = 0.0001,
//                 MaxIterations = 100,
//                 Function = x => x * x - 2
//             };

//             // Act
//             var result = await handler.Handle(command, CancellationToken.None);

//             // Assert
//             Assert.Equal((int)EnumMasterType.MasterType.Success, result.Status.StatusCode);
//             Assert.InRange(result.Data.Result, 1.4142, 1.4143);
//             Assert.True(result.Data.Iterations.Count > 0);
//             Assert.Equal("Bisection method completed successfully.", result.Status.Message);
//         }
//     }
// }