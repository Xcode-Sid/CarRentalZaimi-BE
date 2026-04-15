using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.Ads.Commands.CreateAds;
using CarRentalZaimi.Application.Features.Ads.Commands.DeleteAds;
using CarRentalZaimi.Application.Features.Ads.Commands.UpdateAds;
using CarRentalZaimi.Application.Features.Ads.Queries.GetAllAds;
using CarRentalZaimi.Application.Features.Ads.Queries.GetAllPagedAds;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalZaimi.Application.Services;

public class AdsService(IUnitOfWork _unitOfWork, IMapper _mapper) : IAdsService
{
    public async Task<Result<AdsDto>> CreateAsync(CreateAdsCommand request, CancellationToken cancellationToken = default)
    {
        var newAds = new Ads
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Position = request.Position,
            LinkUrl = request.LinkUrl,
            ImageName = request.ImageName,
            VideoName = request.VideoName,
            ImageUrl = "",
            VideoUrl = "",
            IsActive = request.IsActive,
        };

        await _unitOfWork.Repository<Ads>().AddAsync(newAds, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Handle ImageData
        if (!string.IsNullOrEmpty(request.ImageData) && !string.IsNullOrEmpty(request.ImageName))
        {
            byte[] imageBytes;
            try
            {
                imageBytes = ParseBase64(request.ImageData);
            }
            catch (FormatException)
            {
                return Result<AdsDto>.Error("Invalid base64 data for image.");
            }

            var folderPath = Path.Combine("wwwroot", "images", "ads");
            Directory.CreateDirectory(folderPath);

            var imageName = $"{newAds.Id}_{request.ImageName}";
            var diskPath = Path.Combine(folderPath, imageName);
            var relativePath = Path.Combine("images", "ads", imageName);

            await using (var fileStream = new FileStream(diskPath, FileMode.Create))
            {
                await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length);
            }

            newAds.ImageUrl = relativePath;
        }

        // Handle VideoData
        if (!string.IsNullOrEmpty(request.VideoData)&& !string.IsNullOrEmpty(request.VideoName))
        {
            byte[] videoBytes;
            try
            {
                videoBytes = ParseBase64(request.VideoData);
            }
            catch (FormatException)
            {
                return Result<AdsDto>.Error("Invalid base64 data for video.");
            }

            var folderPath = Path.Combine("wwwroot", "videos", "ads");
            Directory.CreateDirectory(folderPath);

            var videoName = $"{newAds.Id}_{request.VideoName}";
            var diskPath = Path.Combine(folderPath, videoName);
            var relativePath = Path.Combine("videos", "ads", videoName);

            await using (var fileStream = new FileStream(diskPath, FileMode.Create))
            {
                await fileStream.WriteAsync(videoBytes, 0, videoBytes.Length);
            }

            newAds.VideoUrl = relativePath;
        }

        // Save ImageUrl / VideoUrl if anything was set
        if (!string.IsNullOrEmpty(newAds.ImageUrl) || !string.IsNullOrEmpty(newAds.VideoUrl))
        {
            await _unitOfWork.Repository<Ads>().UpdateAsync(newAds, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result<AdsDto>.Success(_mapper.Map<AdsDto>(newAds));
    }

  
    public async Task<Result<bool>> DeleteAsync(DeleteAdsCommand request, CancellationToken cancellationToken = default)
    {
        var existingAds = await _unitOfWork.Repository<Ads>()
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.Id, cancellationToken);

        if (existingAds is null)
            return Result<bool>.Error("This adds id does not exist");

        existingAds.IsDeleted = true;

        await _unitOfWork.Repository<Ads>().UpdateAsync(existingAds, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<AdsDto>>> GetAllAsync(GetAllAdsQuery request, CancellationToken cancellationToken = default)
    {
        var adds = await _unitOfWork.Repository<Ads>()
          .AsQueryable()
          .Where(a => a.IsActive)
          .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<AdsDto>>(adds);
        return Result.Success(mapped);
    }

    public async Task<Result<PagedResponse<AdsDto>>> GetAllPagedAsync(GetAllPagedAdsQuery request, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Ads>()
             .AsQueryable()
             .Where(c => !c.IsDeleted);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(c =>
                (c.Title! != null && c.Title.ToLower().Contains(search)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var ads = await query
            .Skip((request.PageNr - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<AdsDto>>(ads);

        var pagedResponse = new PagedResponse<AdsDto>(mapped, totalCount, request.PageNr, request.PageSize);

        return Result<PagedResponse<AdsDto>>.Success(pagedResponse);
    }

    public async Task<Result<AdsDto>> UpdateAsync(UpdateAdsCommand request, CancellationToken cancellationToken = default)
    {
        var existingAds = await _unitOfWork.Repository<Ads>()
            .FirstOrDefaultAsync(a => a.Id.ToString() == request.Id, cancellationToken);

        if (existingAds is null)
            return Result<AdsDto>.Error("Ad not found.");

        existingAds.Title = request.Title;
        existingAds.Position = request.Position;
        existingAds.LinkUrl = request.LinkUrl;
        existingAds.IsActive = request.IsActive;

        bool hasNewImage = !string.IsNullOrEmpty(request.ImageData) && !string.IsNullOrEmpty(request.ImageName);
        bool hasNewVideo = !string.IsNullOrEmpty(request.VideoData) && !string.IsNullOrEmpty(request.VideoName);

        // ================= DELETE OLD FILES IF SWITCHING =================

        if (hasNewImage && !string.IsNullOrEmpty(existingAds.VideoUrl))
        {
            DeleteFromDisk(existingAds.VideoUrl);
            existingAds.VideoUrl = "";
            existingAds.VideoName = "";
        }

        if (hasNewVideo && !string.IsNullOrEmpty(existingAds.ImageUrl))
        {
            DeleteFromDisk(existingAds.ImageUrl);
            existingAds.ImageUrl = "";
            existingAds.ImageName = "";
        }

        // ================= HANDLE IMAGE =================
        if (hasNewImage)
        {
            DeleteFromDisk(existingAds.ImageUrl);

            var imageBytes = ParseBase64(request.ImageData);

            var folderPath = Path.Combine("wwwroot", "images", "ads");
            Directory.CreateDirectory(folderPath);

            var imageName = $"{existingAds.Id}_{request.ImageName}";
            var diskPath = Path.Combine(folderPath, imageName);
            var relativePath = Path.Combine("images", "ads", imageName);

            await File.WriteAllBytesAsync(diskPath, imageBytes, cancellationToken);

            existingAds.ImageUrl = relativePath;
            existingAds.ImageName = request.ImageName;
        }

        // ================= HANDLE VIDEO =================
        if (hasNewVideo)
        {
            DeleteFromDisk(existingAds.VideoUrl);

            var videoBytes = ParseBase64(request.VideoData);

            var folderPath = Path.Combine("wwwroot", "videos", "ads");
            Directory.CreateDirectory(folderPath);

            var videoName = $"{existingAds.Id}_{request.VideoName}";
            var diskPath = Path.Combine(folderPath, videoName);
            var relativePath = Path.Combine("videos", "ads", videoName);

            await File.WriteAllBytesAsync(diskPath, videoBytes, cancellationToken);

            existingAds.VideoUrl = relativePath;
            existingAds.VideoName = request.VideoName;
        }

        await _unitOfWork.Repository<Ads>().UpdateAsync(existingAds, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AdsDto>.Success(_mapper.Map<AdsDto>(existingAds));
    }


    private static byte[] ParseBase64(string base64Input)
    {
        var base64Data = base64Input.Contains(',')
            ? base64Input.Split(',')[1]
            : base64Input;

        return Convert.FromBase64String(base64Data);
    }
    private static void DeleteFromDisk(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return;

        var fullPath = Path.Combine("wwwroot", relativePath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

}
