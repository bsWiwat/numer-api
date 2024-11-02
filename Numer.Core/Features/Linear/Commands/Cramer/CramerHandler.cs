using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Numer.Core.Features.Linear.Commands.Cramer;
using Numer.Core.Features.Linear.Commands.Determinant;
using Numer.Domain.Common;
using Numer.Domain.Entities;

namespace Numer.Core.Features.MatrixEquations.Commands.CramerRule {
    public class CramerHandler : IRequestHandler<CramerCommand, MatrixResult> {
        private readonly IMediator _mediator;

        public CramerHandler(IMediator mediator) {
            _mediator = mediator;
        }

        public async Task<MatrixResult> Handle(CramerCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            int n = matrixA.Length;

            double detA = await _mediator.Send(new DeterminantCommand { Matrix = matrixA });

            if (detA == 0) {
                return new MatrixResult {
                    Status = new Status {
                        StatusCode = (int)EnumMasterType.MasterType.BadRequest,
                        StatusName = EnumMasterType.MasterType.BadRequest.ToString(),
                        Message = "The determinant of matrix A is zero, so the system has no unique solution."
                    }
                };
            }

            double[] result = new double[n];
            List<double[][]> matrixAiList = new List<double[][]>();
            double[] detAi = new double[n];

            for (int i = 0; i < n; i++) {
                double[][] matrixAi = ReplaceColumn(matrixA, arrayB, i);
                double detAiValue = await _mediator.Send(new DeterminantCommand { Matrix = matrixAi });
                result[i] = detAiValue / detA;

                matrixAiList.Add(matrixAi); // Store matrixAi
                detAi[i] = detAiValue;      // Store detAi
            }

            var matrixData = new MatrixData {
                Result = result,         
                MatrixAi = matrixAiList.ToArray(), 
                DetAi = detAi,           
                DetA = detA              
            };

            return new MatrixResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Computation completed successfully."
                },
                Data = matrixData
            };
        }

        private double[][] ReplaceColumn(double[][] matrix, double[] vector, int colIndex) {
            int n = matrix.Length;
            double[][] newMatrix = new double[n][];

            for (int i = 0; i < n; i++) {
                newMatrix[i] = new double[n];
                for (int j = 0; j < n; j++) {
                    if (j == colIndex) {
                        newMatrix[i][j] = vector[i];
                    }
                    else {
                        newMatrix[i][j] = matrix[i][j];
                    }
                }
            }

            return newMatrix;
        }
    }
}
