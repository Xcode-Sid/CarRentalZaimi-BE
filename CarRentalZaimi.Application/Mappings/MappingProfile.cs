using AutoMapper;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Application.DTOs.ApiResponse;
using CarRentalZaimi.Application.Helpers;
using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // === Lookup Entities ===
        CreateMap<StatePrefix, StatePrefixDto>().ReverseMap();
        CreateMap<CarCategory, CarCategoryDto>().ReverseMap();
        CreateMap<CarCompanyName, CarCompanyNameDto>().ReverseMap();
        CreateMap<CarCompanyModel, CarCompanyModelDto>().ReverseMap();
        CreateMap<CarExteriorColor, CarExteriorColorDto>().ReverseMap();
        CreateMap<CarInteriorColor, CarInteriorColorDto>().ReverseMap();
        CreateMap<CarTransmission, CarTransmissionDto>().ReverseMap();
        CreateMap<CarFuel, CarFuelDto>().ReverseMap();

        // === Car & Images ===
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<CarImages, CarImagesDto>().ReverseMap();

        // === User & Related ===
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
        CreateMap<UserImage, UserImageDto>().ReverseMap();
        CreateMap<UserDevice, UserDeviceDto>().ReverseMap();
        CreateMap<UserNotification, UserNotificationDto>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        CreateMap<PhoneConfirmationToken, PhoneConfirmationTokenDto>().ReverseMap();

        // === Booking & Services ===
        CreateMap<Booking, BookingDto>().ReverseMap();
        CreateMap<BookingService, BookingServiceDto>().ReverseMap();
        CreateMap<AdditionalService, AdditionalServiceDto>().ReverseMap();

        // === Reviews, Saved, Promotions ===
        CreateMap<CarReview, CarReviewDto>().ReverseMap();
        CreateMap<SavedCar, SavedCarDto>().ReverseMap();
        CreateMap<Promotion, PromotionDto>().ReverseMap();

        // === Company & Contact ===
        CreateMap<CompanyProfile, CompanyProfileDto>().ReverseMap();
        CreateMap<ContactMessage, ContactMessageDto>().ReverseMap();

        // === Language & Logs ===
        CreateMap<Language, LanguageDto>().ReverseMap();
        CreateMap<AppLog, AppLogDto>().ReverseMap();

        // === ApiResponse Mappings (with custom converter) ===
        CreateMap<ApiResponse<List<StatePrefix>>, ApiResponse<List<StatePrefixDto>>>()
            .ConvertUsing<ApiResponseConverter<StatePrefix, StatePrefixDto>>();
        CreateMap<ApiResponse<List<CarFuel>>, ApiResponse<List<CarFuelDto>>>()
            .ConvertUsing<ApiResponseConverter<CarFuel, CarFuelDto>>();
        CreateMap<ApiResponse<List<CarTransmission>>, ApiResponse<List<CarTransmissionDto>>>()
            .ConvertUsing<ApiResponseConverter<CarTransmission, CarTransmissionDto>>();
        CreateMap<ApiResponse<List<CarInteriorColor>>, ApiResponse<List<CarInteriorColorDto>>>()
            .ConvertUsing<ApiResponseConverter<CarInteriorColor, CarInteriorColorDto>>();
        CreateMap<ApiResponse<List<CarExteriorColor>>, ApiResponse<List<CarExteriorColorDto>>>()
            .ConvertUsing<ApiResponseConverter<CarExteriorColor, CarExteriorColorDto>>();
        CreateMap<ApiResponse<List<CarCategory>>, ApiResponse<List<CarCategoryDto>>>()
            .ConvertUsing<ApiResponseConverter<CarCategory, CarCategoryDto>>();
        CreateMap<ApiResponse<List<CarCompanyName>>, ApiResponse<List<CarCompanyNameDto>>>()
            .ConvertUsing<ApiResponseConverter<CarCompanyName, CarCompanyNameDto>>();
        CreateMap<ApiResponse<List<CarCompanyModel>>, ApiResponse<List<CarCompanyModelDto>>>()
            .ConvertUsing<ApiResponseConverter<CarCompanyModel, CarCompanyModelDto>>();
    }
}