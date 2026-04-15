using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.CreateOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.DeleteOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Commands.UpdateOccupiedCarDays;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetCarCalendarData;
using CarRentalZaimi.Application.Features.CreateOccupiedCarDays.Queries.GetOccupiedCarDays;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using CarRentalZaimi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalZaimi.Application.Services;

public class OccupiedCarDaysService(IUnitOfWork _unitOfWork, IMapper _mapper) : IOccupiedCarDaysService
{
    public async Task<Result<OccupiedCarDaysDto>> CreateAsync(CreateOccupiedCarDaysCommand request, CancellationToken cancellationToken = default)
    {

        var car = await _unitOfWork.Repository<Car>()
            .FirstOrDefaultAsync(c => c.Id.ToString() == request.CarId);

        if(car == null)
            return Result<OccupiedCarDaysDto>.Error("Car not found");

        if (request.StartDate > request.EndDate)
            return Result<OccupiedCarDaysDto>.Error("StartDate cannot be greater than EndDate");

        var overlap = await _unitOfWork.Repository<OccupiedCarDays>()
            .AsQueryable()
            .AnyAsync(x =>
                x.Car.Id == car.Id &&
                x.StartDate <= request.EndDate &&
                x.EndDate >= request.StartDate,
                cancellationToken);

        if (overlap)
            return Result<OccupiedCarDaysDto>.Error("Car is already occupied in this date range");

        var entity = new OccupiedCarDays
        {
            Id = Guid.NewGuid(),
            Car = car,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Type = Enum.TryParse<CarBlockedDateType>(request.Type, true, out var type)
                ? type
                : CarBlockedDateType.Other
        };

        await _unitOfWork.Repository<OccupiedCarDays>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<OccupiedCarDaysDto>.Success(_mapper.Map<OccupiedCarDaysDto>(entity));
    }

    public async Task<Result<bool>> DeleteAsync(DeleteOccupiedCarDaysCommand request, CancellationToken cancellationToken = default)
    {

        var entity = await _unitOfWork.Repository<OccupiedCarDays>()
            .FirstOrDefaultAsync(x => x.Id.ToString() == request.Id, cancellationToken);

        if (entity == null)
            return Result<bool>.Error("Record not found");

        entity.IsDeleted = true;

        await _unitOfWork.Repository<OccupiedCarDays>().UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<OccupiedCarDaysDto>>> GetCarCalendarDataAsync(GetCarCalendarDataQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<OccupiedCarDays>()
            .AsQueryable()
            .Where(x => x.Car.Id.ToString() == request.CarId && !x.IsDeleted);

        query = query.Where(x =>
            x.EndDate >= request.StartDate &&
            x.StartDate <= request.EndDate);

        var data = await query.ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<OccupiedCarDaysDto>>(data);

        return Result<IEnumerable<OccupiedCarDaysDto>>.Success(mapped);
    }

    public async Task<Result<PagedResponse<OccupiedCarDaysDto>>> GetGetOccupiedCarDaysAsync(GetOccupiedCarDaysQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<OccupiedCarDays>()
            .AsQueryable()
            .Include(o => o.Car)
            .Where(x => !x.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.CarId) &&
            Guid.TryParse(request.CarId, out var carId))
        {
            query = query.Where(x => x.Car.Id == carId);
        }

        if (request.StartDate.HasValue)
            query = query.Where(x => x.EndDate >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(x => x.StartDate <= request.EndDate.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var data = await query
            .OrderByDescending(x => x.StartDate)
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<OccupiedCarDaysDto>>(data);

        return Result<PagedResponse<OccupiedCarDaysDto>>.Success(
            new PagedResponse<OccupiedCarDaysDto>(
                mapped,
                totalCount,
                request.PageNr,
                request.PageSize
            ));
    }

    public async Task<Result<OccupiedCarDaysDto>> UpdateAsync(UpdateOccupiedCarDaysCommand request, CancellationToken cancellationToken = default)
    {

        var entity = await _unitOfWork.Repository<OccupiedCarDays>()
            .FirstOrDefaultAsync(x => x.Id.ToString() == request.Id, cancellationToken);

        if (entity == null)
            return Result<OccupiedCarDaysDto>.Error("Record not found");

        if (request.StartDate > request.EndDate)
            return Result<OccupiedCarDaysDto>.Error("StartDate cannot be greater than EndDate");

        var overlap = await _unitOfWork.Repository<OccupiedCarDays>()
            .AsQueryable()
            .AnyAsync(x =>
                x.Id.ToString() != request.Id &&
                x.Car == entity.Car &&
                x.StartDate <= request.EndDate &&
                x.EndDate >= request.StartDate,
                cancellationToken);

        if (overlap)
            return Result<OccupiedCarDaysDto>.Error("Car is already occupied in this date range");

        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;

        entity.Type = Enum.TryParse<CarBlockedDateType>(request.Type, true, out var type)
            ? type
            : entity.Type;

        await _unitOfWork.Repository<OccupiedCarDays>().UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<OccupiedCarDaysDto>.Success(_mapper.Map<OccupiedCarDaysDto>(entity));
    }
}
