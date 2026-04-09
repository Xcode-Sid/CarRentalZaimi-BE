using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.SavedCar.Command.SaveCar;
using CarRentalZaimi.Application.Features.SavedCar.Queries;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class SavedCarService(IUnitOfWork _unitOfWork, IMapper _mapper) : ISavedCarService
{
    public async Task<Result<PagedResponse<SavedCarDto>>> GetAllSavedCarsAsync(GetAllSavedCarsQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<SavedCar>()
             .AsQueryable()
             .Include(c => c.Car)
             .Include(c => c.User)
             .Where(c => c.User!.Id == request.UserId && !c.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c =>
                (c.Car!.Title != null && c.Car.Title.ToLower().Contains(search)) ||
                (c.Car!.Description != null && c.Car.Description.ToLower().Contains(search)) ||
                (c.Car!.LicensePlate != null && c.Car.LicensePlate.ToLower().Contains(search)) ||
                (c.Car!.Name != null && c.Car.Name.Name.ToLower().Contains(search)) ||
                (c.Car!.Model != null && c.Car.Model.Name.ToLower().Contains(search)) ||
                (c.Car!.Category != null && c.Car.Category.Name.ToLower().Contains(search)));
        }

        if (!string.IsNullOrWhiteSpace(request.CategoryId))
            query = query.Where(c => c.Car!.Category!.Id.ToString() == request.CategoryId);

        var totalCount = await query.CountAsync(cancellationToken);

        var cars = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<SavedCarDto>>(cars);

        // All cars here are saved by definition
        foreach (var car in mapped)
            car.Car!.IsSaved = true;
        var pagedResponse = new PagedResponse<SavedCarDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<SavedCarDto>>.Success(pagedResponse);
    }

    public async Task<Result<bool>> SaveCarAsync(SaveCarCommand request, CancellationToken cancellationToken = default)
    {
        var car = await _unitOfWork.Repository<Car>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.CarId, cancellationToken);

        if (car is null)
            return Result<bool>.Error("Car not found");

        var user = await _unitOfWork.Repository<User>()
           .FirstOrDefaultAsync(p => p.Id == request.UserId, cancellationToken);

        if (user is null)
            return Result<bool>.Error("User not found");

        var savedCar = await _unitOfWork.Repository<SavedCar>()
            .AsQueryable()
            .Include(c => c.Car)
            .Include(c => c.User)
            .FirstOrDefaultAsync(p => p.Car!.Id == car.Id && p.User!.Id == user.Id, cancellationToken);

        if (savedCar is null)
        {
            //save
            var newSavedCar = new SavedCar() { 
                User = user,
                Car = car,
            };
            await _unitOfWork.Repository<SavedCar>().AddAsync(newSavedCar, cancellationToken);
        }
        else
        {
            //unsave
            savedCar.IsDeleted = true;
            await _unitOfWork.Repository<SavedCar>().UpdateAsync(savedCar, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

}
