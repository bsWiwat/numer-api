using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Numer.Core.Features.Extrapolation.Commands.MultipleRegression;
using Numer.Core.Features.Extrapolation.Commands.SimpleRegression;
using Numer.Domain.Entities;

namespace Numer.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ExtrapolationController : ControllerBase {
        private readonly IMediator _mediator;

        public ExtrapolationController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("simpleRegression")]
        public async Task<ActionResult<RegressionResult>> SolveSimpleReg([FromBody] SimpleRegressionCommand request) {
            try {
                var command = new SimpleRegressionCommand {
                    MOrder = request.MOrder,
                    Points = request.Points
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("multipleRegression")]
        public async Task<ActionResult<RegressionResult>> SolveMultiReg([FromBody] MultipleRegressionCommand request) {
            try {
                var command = new MultipleRegressionCommand{
                    ArrayX = request.ArrayX,
                    Points = request.Points
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