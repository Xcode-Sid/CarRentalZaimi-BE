using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
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
    public async Task<Result<CarCompanyModelDto>> CreateAsync(CreateCarCompanyModelCommand request, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Repository<CarCompanyModel>()
         .FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);

        if (company is not null)
            return Result<CarCompanyModelDto>.Error("This car model already exists");

        var companyName = await _unitOfWork.Repository<CarCompanyName>()
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.CompanyNameId!), cancellationToken);


        if (companyName is null)
            return Result<CarCompanyModelDto>.Error("This car company name does not exists");

        var newFuel = new CarCompanyModel
        {
            Id = Guid.NewGuid(),
            CarCompanyName = companyName,
            Name = request.Name,
        };

        await _unitOfWork.Repository<CarCompanyModel>().AddAsync(newFuel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarCompanyModelDto>.Success(_mapper.Map<CarCompanyModelDto>(newFuel));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteCarCompanyModelCommand request, CancellationToken cancellationToken = default)
    {
        var existingCompany = await _unitOfWork.Repository<CarCompanyModel>()
        .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCompany is null)
            return Result<bool>.Error("This car mdel id does not exist");

        existingCompany.IsDeleted = true;

        await _unitOfWork.Repository<CarCompanyModel>().UpdateAsync(existingCompany, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarCompanyModelDto>>> GetAllAsync(GetAllCarCompanyModelQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _unitOfWork.Repository<CarCompanyModel>()
            .AsQueryable()
            .Include(x => x.CarCompanyName)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarCompanyModelDto>>(carColors);
        return Result.Success(mapped);
    }

    public async Task<Result<CarCompanyModelDto>> UpdateAsync(UpdateCarCompanyModelCommand request, CancellationToken cancellationToken = default)
    {
        var existingCompany = await _unitOfWork.Repository<CarCompanyModel>()
            .AsQueryable()
            .Include(x => x.CarCompanyName)
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingCompany is null)
            return Result<CarCompanyModelDto>.Error("This car model id does not exists");

        var company = await _unitOfWork.Repository<CarCompanyModel>()
            .FirstOrDefaultAsync(p => p.Name == request.Name && p.Id.ToString() != request.Id, cancellationToken);

        if (company is not null)
            return Result<CarCompanyModelDto>.Error("This car model already exists");

        existingCompany.Name = request.Name;

        await _unitOfWork.Repository<CarCompanyModel>().UpdateAsync(existingCompany, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CarCompanyModelDto>.Success(_mapper.Map<CarCompanyModelDto>(existingCompany));
    }
}
