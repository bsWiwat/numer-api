using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Numer.Core.Features.Linear.Commands.Cramer;
using Numer.Core.Features.Linear.Commands.GaussElimination;
using Numer.Core.Features.Linear.Commands.GaussJordan;
using Numer.Core.Features.Linear.Commands.JacobiMethod;
using Numer.Domain.Entities;

namespace Numer.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class LinearController : Controller {
        private readonly IMediator _mediator;

        public LinearController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("cramer")]
        public async Task<ActionResult<MatrixResult>> SolveCramer([FromBody] CramerCommand request) {
            try {
                var command = new CramerCommand {
                    MatrixA = request.MatrixA,
                    ArrayB = request.ArrayB,
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("gauss")]
        public async Task<ActionResult<GaussResult>> SolveGauss([FromBody] GaussEliminationCommand request) {
            try {
                var command = new GaussEliminationCommand {
                    MatrixA = request.MatrixA,
                    ArrayB = request.ArrayB,
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("gauss-jordan")]
        public async Task<ActionResult<GaussResult>> SolveGaussJordan([FromBody] GaussJordanCommand request) {
            try {
                var command = new GaussJordanCommand {
                    MatrixA = request.MatrixA,
                    ArrayB = request.ArrayB,
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