using MediatR;
using Microsoft.AspNetCore.Mvc;
using Numer.Core.Features.Interpolation.Commands.LagrangePolynomials;
using Numer.Core.Features.Interpolation.Commands.NewtonDivided;
using Numer.Core.Features.Interpolation.Commands.SplineInterpolation;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class InterpolationController : Controller {
        private readonly IMediator _mediator;

        public InterpolationController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("newton")]
        public async Task<ActionResult<InterpolationResult>> SolveNewton([FromBody] NewtonDividedCommand request) {
            try {
                var command = new NewtonDividedCommand {
                    X = request.X,
                    Points = request.Points
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("lagrange")]
        public async Task<ActionResult<InterpolationResult>> SolveLagrange([FromBody] LagrangePolynomialsCommand request) {
            try {
                var command = new LagrangePolynomialsCommand {
                    X = request.X,
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