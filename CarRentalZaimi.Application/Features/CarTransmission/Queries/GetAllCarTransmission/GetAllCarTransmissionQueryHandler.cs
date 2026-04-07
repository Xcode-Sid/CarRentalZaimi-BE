using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CarFuel.Queries.GetAllCarFuels;
using CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarFuels;
using CarRentalZaimi.Application.Interfaces.Query;
using CarRentalZaimi.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalZaimi.Application.Features.CarTransmission.Queries.GetAllCarTransmission;

internal class GetAllCarTransmissionQueryHandler(ICarTransmissionService _carTransmissionService) : IQueryHandler<GetAllCarTransmissionQuery, IEnumerable<CarTransmissionDto>>
{
    public async Task<Result<IEnumerable<CarTransmissionDto>>> Handle(GetAllCarTransmissionQuery request, CancellationToken cancellationToken)
        => await _carTransmissionService.GetAllAsync(request, cancellationToken);
}
