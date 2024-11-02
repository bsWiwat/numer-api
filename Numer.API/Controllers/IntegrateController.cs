using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NCalc;
using Numer.API.Helper;
using Numer.Core.Features.Integration.Commands.CompositeSimpson;
using Numer.Core.Features.Integration.Commands.CompositeTrapezoidal;
using Numer.Core.Features.Integration.Commands.Trapezoidal;
using Numer.Core.Models;
using Numer.Domain.Entities;

namespace Numer.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class IntegrateController : ControllerBase {
        private readonly IMediator _mediator;

        public IntegrateController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("trapezoidal")]
        public async Task<ActionResult<TrapezoidalResult>> SolveTrapezoidal([FromBody] TrapezoidalRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new TrapezoidalCommand {
                    Function = parsedFunction,
                    XStart = request.XStart,
                    XEnd = request.XEnd,
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("composite-trapezoidal")]
        public async Task<ActionResult<TrapezoidalResult>> SolveCompositeTrapezoidal([FromBody] CompositeTrapezoidalRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new CompositeTrapezoidalCommand {
                    Function = parsedFunction,
                    XStart = request.XStart,
                    XEnd = request.XEnd,
                    NumberIntervals = request.NumberIntervals,
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("composite-simpson")]
        public async Task<ActionResult<TrapezoidalResult>> SolveCompositeSimpson([FromBody] CompositeSimpsonRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new CompositeSimpsonCommand {
                    Function = parsedFunction,
                    XStart = request.XStart,
                    XEnd = request.XEnd,
                    NumberIntervals = request.NumberIntervals,
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
