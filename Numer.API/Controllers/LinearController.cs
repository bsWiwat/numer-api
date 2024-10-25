using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Numer.Core.Features.Linear.Commands.CholeskyDecomposition;
using Numer.Core.Features.Linear.Commands.Cramer;
using Numer.Core.Features.Linear.Commands.GaussElimination;
using Numer.Core.Features.Linear.Commands.GaussJordan;
using Numer.Core.Features.Linear.Commands.GaussSeidel;
using Numer.Core.Features.Linear.Commands.JacobiMethod;
using Numer.Core.Features.Linear.Commands.LUDecomposition;
using Numer.Core.Features.Linear.Commands.MatrixInversion;
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

        [HttpPost("matrix-inversion")]
        public async Task<ActionResult<MatrixInversionResult>> SolveMatrixInversion([FromBody] MatrixInversionCommand request) {
            try {
                var command = new MatrixInversionCommand {
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

        [HttpPost("luDecomposition")]
        public async Task<ActionResult<DecompositionResult>> SolveLUDecomposition([FromBody] LUDecompositionCommand request) {
            try {
                var command = new LUDecompositionCommand {
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

        [HttpPost("choleskyDecomposition")]
        public async Task<ActionResult<DecompositionResult>> SolveCholeskyDecomposition([FromBody] CholeskyDecompositionCommand request) {
            try {
                var command = new CholeskyDecompositionCommand {
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

        [HttpPost("jacobi")]
        public async Task<ActionResult<MatrixIterationResult>> SolveJacobiIteration([FromBody] JacobiCommand request) {
            try {
                var command = new JacobiCommand {
                    MatrixA = request.MatrixA,
                    ArrayB = request.ArrayB,
                    InitialX = request.InitialX,
                    Tolerance = request.Tolerance,
                    MaxIterations = request.MaxIterations,
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("gauss-seidel")]
        public async Task<ActionResult<MatrixIterationResult>> SolveGaussSeidelIteration([FromBody] GaussSeidelCommand request) {
            try {
                var command = new GaussSeidelCommand {
                    MatrixA = request.MatrixA,
                    ArrayB = request.ArrayB,
                    InitialX = request.InitialX,
                    Tolerance = request.Tolerance,
                    MaxIterations = request.MaxIterations,
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