using AutoMapper;
using CarRentalZaimi.Application.Common;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Booking, BookingDto>().ReverseMap();
        CreateMap<BookingService, BookingServiceDto>().ReverseMap();
        CreateMap<AdditionalService, AdditionalServiceDto>().ReverseMap();
        CreateMap<CarCategory, CarCategoryDto>().ReverseMap();
        CreateMap<CarCompanyName, CarCompanyNameDto>().ReverseMap();
        CreateMap<CarCompanyModel, CarCompanyModelDto>().ReverseMap();
        CreateMap<CarExteriorColor, CarExteriorColorDto>().ReverseMap();
        CreateMap<CarInteriorColor, CarInteriorColorDto>().ReverseMap();
        CreateMap<CarTransmission, CarTransmissionDto>().ReverseMap();
        CreateMap<CarFuel, CarFuelDto>().ReverseMap();
        CreateMap<CarImages, CarImagesDto>().ReverseMap();
        CreateMap<CarReview, CarReviewDto>().ReverseMap();
        CreateMap<SavedCar, SavedCarDto>().ReverseMap();
        CreateMap<Promotion, PromotionDto>().ReverseMap();
        CreateMap<CompanyProfile, CompanyProfileDto>().ReverseMap();
        CreateMap<ContactMessage, ContactMessageDto>().ReverseMap();
        CreateMap<Language, LanguageDto>().ReverseMap();
        CreateMap<UserNotification, UserNotificationDto>().ReverseMap();
        CreateMap<UserImage, UserImageDto>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        CreateMap<EmailConfirmationToken, EmailConfirmationTokenDto>().ReverseMap();
        CreateMap<PhoneConfirmationToken, PhoneConfirmationTokenDto>().ReverseMap();
        CreateMap<AppLog, AppLogDto>().ReverseMap();
        CreateMap<UserDevice, UserDeviceDto>().ReverseMap();
        CreateMap<StatePrefix, StatePrefixDto>().ReverseMap();
        CreateMap<Result<List<StatePrefix>>, Result<List<StatePrefixDto>>>();
        CreateMap<Result<List<CarFuel>>, Result<List<CarFuelDto>>>();
        CreateMap<Result<List<CarTransmission>>, Result<List<CarTransmissionDto>>>();
        CreateMap<Result<List<CarInteriorColor>>, Result<List<CarInteriorColorDto>>>();
        CreateMap<Result<List<CarExteriorColor>>, Result<List<CarExteriorColorDto>>>();
        CreateMap<Result<List<CarCategory>>, Result<List<CarCategoryDto>>>();
    }
}
