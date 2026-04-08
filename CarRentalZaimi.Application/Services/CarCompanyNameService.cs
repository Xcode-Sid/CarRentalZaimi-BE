using AutoMapper;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.CreateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.DeleteCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Commands.UpdateCarCompanyName;
using CarRentalZaimi.Application.Features.CarCompanyName.Queries.GetAllCarCompanyName;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarCompanyNameService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarCompanyNameService
{
    public async Task<ApiResponse<CarCompanyNameDto>> CreateAsync(CreateCarCompanyNameCommand request, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Repository<CarCompanyName>()
         .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (company is not null)
            return ApiResponse<CarCompanyNameDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        var newFuel = new CarCompanyName
        {
            Name = request.Name!,
        };

        await _unitOfWork.Repository<CarCompanyName>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarCompanyNameDto>.SuccessResponse(_mapper.Map<CarCompanyNameDto>(newFuel));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(DeleteCarCompanyNameCommand request, CancellationToken cancellationToken = default)
    {
        var existingCompany = await _unitOfWork.Repository<CarCompanyName>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCompany is null)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var isUsedByModel = await _unitOfWork.Repository<CarCompanyModel>()
            .AnyAsync(m => m.CarCompanyName != null && m.CarCompanyName.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByModel)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.INVALID_OPERATION));

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.Name != null && c.Name.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.INVALID_OPERATION));

        existingCompany.IsDeleted = true;

        await _unitOfWork.Repository<CarCompanyName>().UpdateAsync(existingCompany, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<IEnumerable<CarCompanyNameDto>>> GetAllAsync(GetAllCarCompanyNameQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarCompanyName>()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarCompanyNameDto>>(carColors);
        return ApiResponse<IEnumerable<CarCompanyNameDto>>.SuccessResponse(mapped);
    }

    public async Task<ApiResponse<CarCompanyNameDto>> UpdateAsync(UpdateCarCompanyNameCommand request, CancellationToken cancellationToken = default)
    {
        var existingCompany = await _unitOfWork.Repository<CarCompanyName>()
           .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCompany is null)
            return ApiResponse<CarCompanyNameDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var company = await _unitOfWork.Repository<CarCompanyName>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (company is not null)
            return ApiResponse<CarCompanyNameDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        existingCompany.Name = request.Name!;

        await _unitOfWork.Repository<CarCompanyName>().UpdateAsync(existingCompany, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarCompanyNameDto>.SuccessResponse(_mapper.Map<CarCompanyNameDto>(existingCompany));
    }
}

