using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NCalc;
using Numer.API.Helper;
using Numer.Core.Features.RootOfEquation.Commands.BisectionMethod;
using Numer.Core.Features.RootOfEquation.Commands.FalsePositionMethod;
using Numer.Core.Features.RootOfEquation.Commands.GraphicalMethod;
using Numer.Core.Features.RootOfEquation.Commands.NewtonRaphsonMethod;
using Numer.Core.Features.RootOfEquation.Commands.OnePointMethod;
using Numer.Core.Features.RootOfEquation.Commands.SecantMethod;
using Numer.Core.Models;
using Numer.Domain.Entities;

namespace Numer.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RootOfEquationController : ControllerBase {
        private readonly IMediator _mediator;

        public RootOfEquationController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("bisection")]
        public async Task<ActionResult<RootResult>> SolveBisection([FromBody] RootRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new BisectionCommand {
                    Function = parsedFunction,
                    LowerBound = request.LowerBound,
                    UpperBound = request.UpperBound,
                    Tolerance = request.Tolerance,
                    MaxIterations = request.MaxIterations
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("falsePosition")]
        public async Task<ActionResult<RootResult>> SolveFalsePosition([FromBody] RootRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new FalsePositionCommand {
                    Function = parsedFunction,
                    LowerBound = request.LowerBound,            
                    UpperBound = request.UpperBound,
                    Tolerance = request.Tolerance,
                    MaxIterations = request.MaxIterations
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("graphical")]
        public async Task<ActionResult<RootResult>> SolveGraphical([FromBody] RootRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new GraphicalCommand {
                    Function = parsedFunction,
                    LowerBound = request.LowerBound,
                    UpperBound = request.UpperBound,
                    Tolerance = request.Tolerance,
                    MaxIterations = request.MaxIterations
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("secant")]
        public async Task<ActionResult<RootResult>> SolveSecant([FromBody] RootRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new SecantCommand {
                    Function = parsedFunction,
                    LowerBound = request.LowerBound,
                    UpperBound = request.UpperBound,
                    Tolerance = request.Tolerance,
                    MaxIterations = request.MaxIterations
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("one-point")]
        public async Task<ActionResult<RootResult>> SolveOnePoint([FromBody] RootRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new OnePointCommand {
                    Function = parsedFunction,
                    XInitial = request.UpperBound,
                    Tolerance = request.Tolerance,
                    MaxIterations = request.MaxIterations
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("newton")]
        public async Task<ActionResult<RootResult>> SolveNewton([FromBody] RootRequest request) {
            try {
                string parsedExpression = ExpressionUtils.ConvertExponentiation(request.FunctionExpression);

                Func<double, double> parsedFunction = x => {
                    var expression = new Expression(parsedExpression);
                    expression.Parameters["x"] = x;
                    return Convert.ToDouble(expression.Evaluate());
                };

                var command = new NewtonRaphsonCommand {
                    Function = parsedFunction,
                    XInitial = request.UpperBound,
                    Tolerance = request.Tolerance,
                    MaxIterations = request.MaxIterations
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
