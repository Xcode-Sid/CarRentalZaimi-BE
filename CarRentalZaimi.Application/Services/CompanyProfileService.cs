using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.Features.CompanyProfile.Commands.AddCompanyProfileData;
using CarRentalZaimi.Application.Features.CompanyProfile.Queries.GetCompanyProfileData;
using CarRentalZaimi.Application.Interfaces.Services;
using CarRentalZaimi.Application.Interfaces.UnitOfWork;
using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Services;

public class CompanyProfileService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICompanyProfileService
{
    public async Task<Result<CompanyProfileDto>> AddCompanyProfileDataAsync(AddCompanyProfileDataCommand request, CancellationToken cancellationToken = default)
    {
        // Save the logo and get its relative path
        string? logoPath = null;
        if (!string.IsNullOrEmpty(request.LogoUrl))
        {
            logoPath = await SaveCompanyLogoAsync( request.LogoUrl, cancellationToken);
        }

        var existingProfile = await _unitOfWork.Repository<CompanyProfile>()
            .FirstOrDefaultAsync(x => !x.IsDeleted, cancellationToken);

        if (existingProfile is null)
        {
            var profile = new CompanyProfile
            {
                Name = request.Name,
                LogoUrl = logoPath, 
                Tagline = request.Tagline,
                AboutText = request.AboutText,
                MissionTitle = request.MissionTitle,
                MissionDescription = request.MissionDescription,
                WhyChooseUs = request.WhyChooseUs,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                WorkingHours = request.WorkingHours,
                FacebookUrl = request.FacebookUrl,
                InstagramUrl = request.InstagramUrl,
                TwiterUrl = request.TwiterUrl,
                YoutubeUrl = request.YoutubeUrl,
                WhatsAppNumber = request.WhatsAppNumber,
            };

            await _unitOfWork.Repository<CompanyProfile>().AddAsync(profile, cancellationToken);
        }
        else
        {
            existingProfile.Name = request.Name;

            // Only update the logo if a new one was uploaded
            if (logoPath is not null)
            {
                DeleteOldLogo(existingProfile.LogoUrl); // optional: clean up old file
                existingProfile.LogoUrl = logoPath;
            }

            existingProfile.Tagline = request.Tagline;
            existingProfile.AboutText = request.AboutText;
            existingProfile.MissionTitle = request.MissionTitle;
            existingProfile.MissionDescription = request.MissionDescription;
            existingProfile.WhyChooseUs = request.WhyChooseUs;
            existingProfile.Email = request.Email;
            existingProfile.Phone = request.Phone;
            existingProfile.Address = request.Address;
            existingProfile.WorkingHours = request.WorkingHours;
            existingProfile.FacebookUrl = request.FacebookUrl;
            existingProfile.InstagramUrl = request.InstagramUrl;
            existingProfile.TwiterUrl = request.TwiterUrl;
            existingProfile.YoutubeUrl = request.YoutubeUrl;
            existingProfile.WhatsAppNumber = request.WhatsAppNumber;

            await _unitOfWork.Repository<CompanyProfile>().UpdateAsync(existingProfile);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = existingProfile is null
            ? await _unitOfWork.Repository<CompanyProfile>().FirstOrDefaultAsync(x => !x.IsDeleted, cancellationToken)
            : existingProfile;

        var dto = _mapper.Map<CompanyProfileDto>(result);
        return Result<CompanyProfileDto>.Success(dto);
    }

    // Optional: deletes the old logo file from disk when updating
    private void DeleteOldLogo(string? relativePath)
    {
        if (string.IsNullOrEmpty(relativePath)) return;

        var fullPath = Path.Combine("wwwroot", relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    public async Task<Result<CompanyProfileDto>> GetCompanyProfileDataAsync(GetCompanyProfileDataQuery request, CancellationToken cancellationToken = default)
    {
        var existingProfile = await _unitOfWork.Repository<CompanyProfile>()
           .FirstOrDefaultAsync(x => !x.IsDeleted, cancellationToken);

        if(existingProfile == null)
            return Result<CompanyProfileDto>.Error("Company profile not found");

        return Result<CompanyProfileDto>.Success(_mapper.Map<CompanyProfileDto>(existingProfile));
    }

    private async Task<string> SaveCompanyLogoAsync(string base64Data, CancellationToken cancellationToken = default)
    {
        var folderPath = Path.Combine("wwwroot", "images", "company");
        Directory.CreateDirectory(folderPath);

        byte[] imageData = Convert.FromBase64String(base64Data);

        // Add a unique suffix to avoid overwriting previous logos
        var uniqueName = $"{Guid.NewGuid()}_{"company_logo"}";
        var imagePath = Path.Combine(folderPath, uniqueName);

        await using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await fileStream.WriteAsync(imageData, 0, imageData.Length, cancellationToken);
        }

        // Return the relative path to store in the database
        return $"images/company/{uniqueName}";
    }
}
