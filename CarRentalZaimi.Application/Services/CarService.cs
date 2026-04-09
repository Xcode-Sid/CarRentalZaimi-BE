using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.Common.Model;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Cars.Commands.AddFeaturedCar;
using CarRentalZaimi.Application.Features.Cars.Commands.CreateCar;
using CarRentalZaimi.Application.Features.Cars.Commands.DeleteCar;
using CarRentalZaimi.Application.Features.Cars.Commands.UpdateCar;
using CarRentalZaimi.Application.Features.Cars.Queries.GetAllCars;
using CarRentalZaimi.Application.Features.Cars.Queries.GetCarById;
using CarRentalZaimi.Application.Features.Cars.Queries.GetFeaturedCars;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRentalZaimi.Application.Services;

public class CarService(
    IUnitOfWork _uow,
    IMapper _mapper,
    ILogger<CarService> _logger) : ICarService
{

    public async Task<Result<CarDto>> CreateCarAsync(CreateCarCommand request, CancellationToken cancellationToken)
    {
        // Check if a car with the same license plate already exists
        if (request.LicensePlate is not null)
        {
            var existingCar = await _uow.Repository<Car>()
                .FirstOrDefaultAsync(c => c.LicensePlate == request.LicensePlate, cancellationToken);

            if (existingCar is not null)
                return Result<CarDto>.Error("A car with this license plate already exists.");
        }

        var newCar = new Car
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Year = request.Year,
            LicensePlate = request.LicensePlate,
            PricePerDay = request.PricePerDay,
            Seats = request.Seats,
            Doors = request.Doors,
            Mileage = request.Mileage,
            HorsePower = request.HorsePower,

            //bool properties
            ABS = request.ABS,
            Bluetooth = request.Bluetooth,
            ParkingSensors = request.ParkingSensors,
            CruiseControl = request.CruiseControl,
            ClimateControl = request.ClimateControl,
            LEDHeadlights = request.LEDHeadlights,
            AppleCarPlay = request.AppleCarPlay,
            AndroidAuto = request.AndroidAuto,
            LaneDepartureAlert = request.LaneDepartureAlert,
            AdaptiveCruiseControl = request.AdaptiveCruiseControl,
            ToyotaSafetySense = request.ToyotaSafetySense,
            HeatedSeats = request.HeatedSeats,
            PanoramicRoof = request.PanoramicRoof,
            ThirdRowSeats = request.ThirdRowSeats,
            WirelessCharging = request.HeatedSeats,
            Camera = request.Camera,
            AirConditioner = request.AirConditioner,
            ElectricWindows = request.ElectricWindows,
            GPS = request.GPS,
        };

        // Resolve foreign key navigation properties if IDs are provided
        if (request.CategoryId.HasValue)
        {
            var category = await _uow.Repository<CarCategory>()
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId.Value, cancellationToken);

            if (category is null)
                return Result<CarDto>.Error("Category not found.");

            newCar.Category = category;
        }

        if (request.NameId.HasValue)
        {
            var name = await _uow.Repository<CarCompanyName>()
                .FirstOrDefaultAsync(n => n.Id == request.NameId.Value, cancellationToken);

            if (name is null)
                return Result<CarDto>.Error("Car company name not found.");

            newCar.Name = name;
        }

        if (request.ModelId.HasValue)
        {
            var model = await _uow.Repository<CarCompanyModel>()
                .FirstOrDefaultAsync(m => m.Id == request.ModelId.Value, cancellationToken);

            if (model is null)
                return Result<CarDto>.Error("Car model not found.");

            newCar.Model = model;
        }

        if (request.ExteriorColorTypeId.HasValue)
        {
            var exteriorColor = await _uow.Repository<CarExteriorColor>()
                .FirstOrDefaultAsync(e => e.Id == request.ExteriorColorTypeId.Value, cancellationToken);

            if (exteriorColor is null)
                return Result<CarDto>.Error("Exterior color type not found.");

            newCar.ExteriorColorType = exteriorColor;
        }

        if (request.InteriorColorTypeId.HasValue)
        {
            var interiorColor = await _uow.Repository<CarInteriorColor>()
                .FirstOrDefaultAsync(i => i.Id == request.InteriorColorTypeId.Value, cancellationToken);

            if (interiorColor is null)
                return Result<CarDto>.Error("Interior color type not found.");

            newCar.InteriorColorType = interiorColor;
        }

        if (request.TransmissionTypeId.HasValue)
        {
            var transmission = await _uow.Repository<CarTransmission>()
                .FirstOrDefaultAsync(t => t.Id == request.TransmissionTypeId.Value, cancellationToken);

            if (transmission is null)
                return Result<CarDto>.Error("Transmission type not found.");

            newCar.TransmissionType = transmission;
        }

        if (request.FuelTypeId.HasValue)
        {
            var fuel = await _uow.Repository<CarFuel>()
                .FirstOrDefaultAsync(f => f.Id == request.FuelTypeId.Value, cancellationToken);

            if (fuel is null)
                return Result<CarDto>.Error("Fuel type not found.");

            newCar.FuelType = fuel;
        }

        await _uow.Repository<Car>().AddAsync(newCar, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        // Add images if provided
        if (request.CarImages is not null && request.CarImages.Count > 0)
            await AddCarImagesAsync(newCar, request.CarImages, cancellationToken);


        await _uow.SaveChangesAsync(cancellationToken);
        return Result<CarDto>.Success(_mapper.Map<CarDto>(newCar));
    }


    public async Task<Result<CarDto>> UpdateCarAsync(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var existingCar = await _uow.Repository<Car>()
            .FirstOrDefaultAsync(c => c.Id.ToString() == request.CarId, cancellationToken);

        if (existingCar is null)
            return Result<CarDto>.Error("Car not found.");

        // Check license plate uniqueness against other cars
        if (request.LicensePlate is not null)
        {
            var plateExists = await _uow.Repository<Car>()
                .FirstOrDefaultAsync(c => c.LicensePlate == request.LicensePlate && c.Id.ToString() != request.CarId, cancellationToken);

            if (plateExists is not null)
                return Result<CarDto>.Error("A car with this license plate already exists.");
        }

        // Update scalar properties
        existingCar.Title = request.Title;
        existingCar.Description = request.Description;
        existingCar.Year = request.Year;
        existingCar.LicensePlate = request.LicensePlate;
        existingCar.PricePerDay = request.PricePerDay;
        existingCar.Seats = request.Seats;
        existingCar.Doors = request.Doors;
        existingCar.Mileage = request.Mileage;
        existingCar.HorsePower = request.HorsePower;

        //bool properties
        existingCar.ABS = request.ABS;
        existingCar.Bluetooth = request.Bluetooth;
        existingCar.ParkingSensors = request.ParkingSensors;
        existingCar.CruiseControl = request.CruiseControl;
        existingCar.ClimateControl = request.ClimateControl;
        existingCar.LEDHeadlights = request.LEDHeadlights;
        existingCar.AppleCarPlay = request.AppleCarPlay;
        existingCar.AndroidAuto = request.AndroidAuto;
        existingCar.LaneDepartureAlert = request.LaneDepartureAlert;
        existingCar.AdaptiveCruiseControl = request.AdaptiveCruiseControl;
        existingCar.ToyotaSafetySense = request.ToyotaSafetySense;
        existingCar.HeatedSeats = request.HeatedSeats;
        existingCar.PanoramicRoof = request.PanoramicRoof;
        existingCar.ThirdRowSeats = request.ThirdRowSeats;
        existingCar.WirelessCharging = request.HeatedSeats;
        existingCar.Camera = request.Camera;
        existingCar.AirConditioner = request.AirConditioner;
        existingCar.ElectricWindows = request.ElectricWindows;
        existingCar.GPS = request.GPS;

        // Resolve navigation properties
        if (request.CategoryId.HasValue)
        {
            var category = await _uow.Repository<CarCategory>()
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId.Value, cancellationToken);

            if (category is null)
                return Result<CarDto>.Error("Category not found.");

            existingCar.Category = category;
        }

        if (request.NameId.HasValue)
        {
            var name = await _uow.Repository<CarCompanyName>()
                .FirstOrDefaultAsync(n => n.Id == request.NameId.Value, cancellationToken);

            if (name is null)
                return Result<CarDto>.Error("Car company name not found.");

            existingCar.Name = name;
        }

        if (request.ModelId.HasValue)
        {
            var model = await _uow.Repository<CarCompanyModel>()
                .FirstOrDefaultAsync(m => m.Id == request.ModelId.Value, cancellationToken);

            if (model is null)
                return Result<CarDto>.Error("Car model not found.");

            existingCar.Model = model;
        }

        if (request.ExteriorColorTypeId.HasValue)
        {
            var exteriorColor = await _uow.Repository<CarExteriorColor>()
                .FirstOrDefaultAsync(e => e.Id == request.ExteriorColorTypeId.Value, cancellationToken);

            if (exteriorColor is null)
                return Result<CarDto>.Error("Exterior color type not found.");

            existingCar.ExteriorColorType = exteriorColor;
        }

        if (request.InteriorColorTypeId.HasValue)
        {
            var interiorColor = await _uow.Repository<CarInteriorColor>()
                .FirstOrDefaultAsync(i => i.Id == request.InteriorColorTypeId.Value, cancellationToken);

            if (interiorColor is null)
                return Result<CarDto>.Error("Interior color type not found.");

            existingCar.InteriorColorType = interiorColor;
        }

        if (request.TransmissionTypeId.HasValue)
        {
            var transmission = await _uow.Repository<CarTransmission>()
                .FirstOrDefaultAsync(t => t.Id == request.TransmissionTypeId.Value, cancellationToken);

            if (transmission is null)
                return Result<CarDto>.Error("Transmission type not found.");

            existingCar.TransmissionType = transmission;
        }

        if (request.FuelTypeId.HasValue)
        {
            var fuel = await _uow.Repository<CarFuel>()
                .FirstOrDefaultAsync(f => f.Id == request.FuelTypeId.Value, cancellationToken);

            if (fuel is null)
                return Result<CarDto>.Error("Fuel type not found.");

            existingCar.FuelType = fuel;
        }

        // Handle image replacement if new images are provided
        if (request.CarImages is not null && request.CarImages.Count > 0)
            await UpdateCarImagesAsync(existingCar, request.CarImages, cancellationToken);

        await _uow.Repository<Car>().UpdateAsync(existingCar, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Result<CarDto>.Success(_mapper.Map<CarDto>(existingCar));
    }

    public async Task<Result<bool>> DeleteCarAsync(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var existingCar = await _uow.Repository<Car>()
            .FirstOrDefaultAsync(c => c.Id.ToString() == request.Id, cancellationToken);

        if (existingCar is null)
            return Result<bool>.Error("Car not found.");

        // Soft-delete all associated images and remove files from disk
        var carImages = await _uow.Repository<CarImages>()
            .AsQueryable()
            .Where(i => i.Car!.Id == existingCar.Id && !i.IsDeleted)
            .ToListAsync(cancellationToken);

        foreach (var image in carImages)
        {
            image.IsDeleted = true;

            if (!string.IsNullOrEmpty(image.ImagePath))
            {
                var filePath = Path.Combine("wwwroot", image.ImagePath);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        existingCar.IsDeleted = true;

        await _uow.Repository<Car>().UpdateAsync(existingCar, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<CarDto>> GetCarByIdAsync(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var car = await _uow.Repository<Car>()
            .AsQueryable()
            .Include(c => c.Category)
            .Include(c => c.Name)
            .Include(c => c.Model)
            .Include(c => c.ExteriorColorType)
            .Include(c => c.InteriorColorType)
            .Include(c => c.TransmissionType)
            .Include(c => c.FuelType)
            .Include(c => c.CarImages!.Where(c => !c.IsDeleted))
            .Include(c => c.CarReviews!.Where(r => !r.IsDeleted))
            .FirstOrDefaultAsync(c => c.Id.ToString() == request.Id && !c.IsDeleted, cancellationToken);

        if (car is null)
            return Result<CarDto>.Error("Car not found.");

        var carDto = _mapper.Map<CarDto>(car);
        carDto.TotalReviews = car.CarReviews?.Count;

        return Result<CarDto>.Success(carDto);
    }

    public async Task<Result<PagedResponse<CarDto>>> GetAllPagedCarsAsync(GetAllPagedCarsQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<Car>()
            .AsQueryable()
            .Include(c => c.Category)
            .Include(c => c.Name)
            .Include(c => c.Model)
            .Include(c => c.ExteriorColorType)
            .Include(c => c.InteriorColorType)
            .Include(c => c.TransmissionType)
            .Include(c => c.FuelType)
            .Include(c => c.CarImages!.Where(c => !c.IsDeleted))
            .Where(c => !c.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c =>
                (c.Title != null && c.Title.ToLower().Contains(search)) ||
                (c.Description != null && c.Description.ToLower().Contains(search)) ||
                (c.LicensePlate != null && c.LicensePlate.ToLower().Contains(search)) ||
                (c.Name != null && c.Name.Name.ToLower().Contains(search)) ||
                (c.Model != null && c.Model.Name.ToLower().Contains(search)) ||
                (c.Category != null && c.Category.Name.ToLower().Contains(search)));
        }


        if (!string.IsNullOrWhiteSpace(request.CategoryId))
            query = query.Where(c => c.Category!.Id.ToString() == request.CategoryId);

        if (!string.IsNullOrWhiteSpace(request.TransmissionId))
            query = query.Where(c => c.TransmissionType!.Id.ToString() == request.TransmissionId);

        if (!string.IsNullOrWhiteSpace(request.FuelTypeId))
            query = query.Where(c => c.FuelType!.Id.ToString() == request.FuelTypeId);


        if (request.Seats.HasValue)
            query = request.Seats.Value == 7
                ? query.Where(c => c.Seats >= 7)
                : query.Where(c => c.Seats == request.Seats.Value);

        if (request.PriceMin.HasValue)
            query = query.Where(c => c.PricePerDay >= request.PriceMin.Value);

        if (request.PriceMax.HasValue)
            query = query.Where(c => c.PricePerDay <= request.PriceMax.Value);


        query = request.SortBy switch
        {
            "priceAsc" => query.OrderBy(c => c.PricePerDay),
            "priceDesc" => query.OrderByDescending(c => c.PricePerDay),
            "newest" => query.OrderByDescending(c => c.Year),
            _ => query.OrderBy(c => c.PricePerDay), // recommended / default
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var cars = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<CarDto>>(cars);
        // ✅ Populate IsSaved after mapping
        if (!string.IsNullOrWhiteSpace(request.UserId))
        {
            var savedCarIds = await _uow.Repository<SavedCar>()
                .AsQueryable()
                .Where(sc => sc.User!.Id == request.UserId && !sc.IsDeleted)
                .Select(sc => sc.Car!.Id)
                .ToHashSetAsync(cancellationToken);

            foreach (var car in mapped)
                car.IsSaved = savedCarIds.Contains(car.Id); // in-memory, no EF translation issue
        }

        var pagedResponse = new PagedResponse<CarDto>(mapped, totalCount, request.PageNr, request.PageSize);
        return Result<PagedResponse<CarDto>>.Success(pagedResponse);
    }

    public async Task<Result<IEnumerable<CarDto>>> GetAllCarsAsync(GetAllCarsQuery request, CancellationToken cancellationToken = default)
    {
        var carColors = await _uow.Repository<Car>()
         .AsQueryable()
         .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CarDto>>(carColors);
        return Result.Success(mapped);
    }


    public async Task<Result<bool>> AddFeaturedCarAsync(AddFeaturedCarCommand request, CancellationToken cancellationToken = default)
    {
        var existingCar = await _uow.Repository<Car>()
         .FirstOrDefaultAsync(c => c.Id.ToString() == request.CarId, cancellationToken);

        if (existingCar is null)
            return Result<bool>.Error("Car not found.");

        existingCar.IsRecommended = request.IsRecommended;

        await _uow.Repository<Car>().UpdateAsync(existingCar, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<CarDto>>> GetFeaturedCarsAsync(GetFeaturedCarsQuery request, CancellationToken cancellationToken = default)
    {
        var cars = await GetBaseCarQuery()
            .Where(c => c.CarReviews.Any(r => !r.IsDeleted))
            .OrderByDescending(c => c.CarReviews.Count(r => !r.IsDeleted))
            .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);

        if (cars.Count == 0)
        {
            cars = await GetBaseCarQuery()
                .Where(c => c.IsRecommended)
                .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                .Take(request.Limit)
                .ToListAsync(cancellationToken);
        }

        return Result<IEnumerable<CarDto>>.Success(cars);
    }



    private IQueryable<Car> GetBaseCarQuery()
    {
        return _uow.Repository<Car>()
            .AsQueryable()
            .Where(c => !c.IsDeleted);
    }

    private async Task AddCarImagesAsync(Car car, List<CarImagesCommand> images, CancellationToken cancellationToken)
    {
        var folderName = $"{car.Id}";
        var folderPath = Path.Combine("wwwroot", "images", "cars", folderName);
        Directory.CreateDirectory(folderPath);

        var primaryImageName = images.FirstOrDefault(i => i.IsPrimary)?.Name;

        var carImages = new List<CarImages>();

        foreach (var image in images)
        {
            if (string.IsNullOrEmpty(image.Name))
                continue;

            byte[] imageData;
            try
            {
                imageData = Convert.FromBase64String(image.Data!);
            }
            catch (FormatException)
            {
                _logger.LogWarning("Invalid base64 data for image '{ImageName}'.", image.Name);
                continue;
            }

            var diskPath = Path.Combine(folderPath, image.Name);
            var relativePath = Path.Combine("images", "cars", folderName, image.Name);

            await using (var fileStream = new FileStream(diskPath, FileMode.Create))
            {
                await fileStream.WriteAsync(imageData, 0, imageData.Length);
            }

            carImages.Add(new CarImages
            {
                Id = Guid.NewGuid(),
                Car = car,
                ImageName = image.Name,
                ImagePath = relativePath,
                IsPrimary = image.Name == primaryImageName,
            });
        }

        // Fallback: mark first image as primary if none was flagged
        if (carImages.Count > 0 && !carImages.Any(i => i.IsPrimary))
            carImages[0].IsPrimary = true;

        await _uow.Repository<CarImages>().AddRangeAsync(carImages, cancellationToken);
    }

    private async Task UpdateCarImagesAsync(Car car, List<CarImagesCommand> images, CancellationToken cancellationToken)
    {
        var folderName = $"{car.Id}";
        var folderPath = Path.Combine("wwwroot", "images", "cars", folderName);
        Directory.CreateDirectory(folderPath);

        // Get all existing non-deleted images for this car
        var existingImages = await _uow.Repository<CarImages>()
            .AsQueryable()
            .Where(i => i.Car!.Id == car.Id && !i.IsDeleted)
            .ToListAsync(cancellationToken);

        // Soft-delete images that are no longer in the incoming list
        var incomingImageNames = images
            .Where(i => !string.IsNullOrEmpty(i.Name))
            .Select(i => i.Name)
            .ToHashSet();

        foreach (var imageToDelete in existingImages.Where(x => !incomingImageNames.Contains(x.ImageName)))
        {
            imageToDelete.IsDeleted = true;

            if (!string.IsNullOrEmpty(imageToDelete.ImagePath))
            {
                var oldFilePath = Path.Combine("wwwroot", imageToDelete.ImagePath);
                if (File.Exists(oldFilePath))
                    File.Delete(oldFilePath);
            }
        }

        // Determine new primary
        var primaryImageName = images.FirstOrDefault(i => i.IsPrimary)?.Name;

        foreach (var image in images)
        {
            if (string.IsNullOrEmpty(image.Name))
                continue;

            bool isPrimary = image.Name == primaryImageName;

            var existingImage = existingImages.FirstOrDefault(x => x.ImageName == image.Name);

            // No base64 data means image already exists — only update IsPrimary
            if (string.IsNullOrEmpty(image.Data))
            {
                if (existingImage is null)
                {
                    _logger.LogWarning("Image '{ImageName}' not found in database.", image.Name);
                    continue;
                }

                existingImage.IsPrimary = isPrimary;
                await _uow.Repository<CarImages>().UpdateAsync(existingImage, cancellationToken);
                continue;
            }

            byte[] imageData;
            try
            {
                imageData = Convert.FromBase64String(image.Data);
            }
            catch (FormatException)
            {
                _logger.LogWarning("Invalid base64 data for image '{ImageName}'.", image.Name);
                continue;
            }

            var diskPath = Path.Combine(folderPath, image.Name);
            var relativePath = Path.Combine("images", "cars", folderName, image.Name);

            if (existingImage is not null)
            {
                // Delete old file from disk
                if (!string.IsNullOrEmpty(existingImage.ImagePath))
                {
                    var oldFilePath = Path.Combine("wwwroot", existingImage.ImagePath);
                    if (File.Exists(oldFilePath))
                        File.Delete(oldFilePath);
                }

                await using (var fileStream = new FileStream(diskPath, FileMode.Create))
                {
                    await fileStream.WriteAsync(imageData, 0, imageData.Length);
                }

                existingImage.ImageName = image.Name;
                existingImage.ImagePath = relativePath;
                existingImage.IsPrimary = isPrimary;
                existingImage.IsDeleted = false;

                await _uow.Repository<CarImages>().UpdateAsync(existingImage, cancellationToken);
            }
            else
            {
                await using (var fileStream = new FileStream(diskPath, FileMode.Create))
                {
                    await fileStream.WriteAsync(imageData, 0, imageData.Length);
                }

                var newImage = new CarImages
                {
                    Id = Guid.NewGuid(),
                    Car = car,
                    ImageName = image.Name,
                    ImagePath = relativePath,
                    IsPrimary = isPrimary,
                };

                await _uow.Repository<CarImages>().AddAsync(newImage, cancellationToken);
            }
        }
    }

}