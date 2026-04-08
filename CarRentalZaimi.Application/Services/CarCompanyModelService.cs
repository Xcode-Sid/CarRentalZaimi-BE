using AutoMapper;
using CarRentalZaimi.Application.Common.Errors;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.CreateCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.DeleteCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Commands.UpdateCarCompanyModel;
using CarRentalZaimi.Application.Features.CarCompanyModel.Queries.GetAllCarCompanyModel;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class CarCompanyModelService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICarCompanyModelService
{
    public async Task<ApiResponse<CarCompanyModelDto>> CreateAsync(CreateCarCompanyModelCommand request, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Repository<CarCompanyModel>()
         .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (company is not null)
            return ApiResponse<CarCompanyModelDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        var companyName = await _unitOfWork.Repository<CarCompanyName>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.CompanyNameId, cancellationToken);


        if (companyName is null)
            return ApiResponse<CarCompanyModelDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var newFuel = new CarCompanyModel
        {
            CarCompanyName = companyName,
            Name = request.Name!,
        };

        await _unitOfWork.Repository<CarCompanyModel>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarCompanyModelDto>.SuccessResponse(_mapper.Map<CarCompanyModelDto>(newFuel));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(DeleteCarCompanyModelCommand request, CancellationToken cancellationToken = default)
    {
        var existingCompany = await _unitOfWork.Repository<CarCompanyModel>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCompany is null)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var isUsedByCar = await _unitOfWork.Repository<Car>()
            .AnyAsync(c => c.Model != null && c.Model.Id.ToString() == request.Id, cancellationToken);

        if (isUsedByCar)
            return ApiResponse<bool>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.INVALID_OPERATION));

        existingCompany.IsDeleted = true;

        await _unitOfWork.Repository<CarCompanyModel>().UpdateAsync(existingCompany, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.SuccessResponse(true);
    }

    public async Task<ApiResponse<IEnumerable<CarCompanyModelDto>>> GetAllAsync(GetAllCarCompanyModelQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarCompanyModel>()
            .AsQueryable()
            .Include(x => x.CarCompanyName)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarCompanyModelDto>>(carColors);
        return ApiResponse<IEnumerable<CarCompanyModelDto>>.SuccessResponse(mapped);
    }

    public async Task<ApiResponse<CarCompanyModelDto>> UpdateAsync(UpdateCarCompanyModelCommand request, CancellationToken cancellationToken = default)
    {
        var existingCompany = await _unitOfWork.Repository<CarCompanyModel>()
            .AsQueryable()
            .Include(x => x.CarCompanyName)
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCompany is null)
            return ApiResponse<CarCompanyModelDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.NOT_FOUND));

        var company = await _unitOfWork.Repository<CarCompanyModel>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (company is not null)
            return ApiResponse<CarCompanyModelDto>.FailureResponse(ErrorMessages.GetMessage(ErrorCodes.ALREADY_EXISTS));

        existingCompany.Name = request.Name!;

        await _unitOfWork.Repository<CarCompanyModel>().UpdateAsync(existingCompany, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<CarCompanyModelDto>.SuccessResponse(_mapper.Map<CarCompanyModelDto>(existingCompany));
    }
}

