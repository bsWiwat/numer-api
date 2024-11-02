using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NCalc;
using Numer.API.Helper;
using Numer.Core.Features.Differentiation.Commands.NumericalDifferentiation;
using Numer.Core.Models;
using Numer.Domain.Entities;

namespace Numer.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class DifferentiationController : ControllerBase {
        private readonly IMediator _mediator;

        public DifferentiationController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("differentiation")]
        public async Task<ActionResult<RegressionResult>> SolveDifferentiation([FromBody] NumericalDifferentiationRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new NumericalDifferentiationCommand {
                    Function = parsedFunction,
                    X = request.X,
                    H = request.H,
                    Order = request.Order,
                    Error = request.Error,
                    Direction = request.Direction
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}