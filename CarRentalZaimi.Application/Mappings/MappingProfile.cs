using AutoMapper;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<List<Car>, List<CarDto>>();
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
        CreateMap<List<User>, List<UserDto>>();
        CreateMap<Booking, BookingDto>().ReverseMap();
        CreateMap<List<Booking>, List<BookingDto>>();
        CreateMap<BookingService, BookingServiceDto>().ReverseMap();
        CreateMap<List<BookingService>, List<BookingServiceDto>>();
        CreateMap<AdditionalService, AdditionalServiceDto>().ReverseMap();
        CreateMap<List<AdditionalService>, List<AdditionalServiceDto>>();
        CreateMap<CarCategory, CarCategoryDto>().ReverseMap();
        CreateMap<List<CarCategory>, List<CarCategoryDto>>();
        CreateMap<CarCompanyName, CarCompanyNameDto>().ReverseMap();
        CreateMap<List<CarCompanyName>, List<CarCompanyNameDto>>();
        CreateMap<CarCompanyModel, CarCompanyModelDto>().ReverseMap();
        CreateMap<List<CarCompanyModel>, List<CarCompanyModelDto>>();
        CreateMap<CarExteriorColor, CarExteriorColorDto>().ReverseMap();
        CreateMap<List<CarExteriorColor>, List<CarExteriorColorDto>>();
        CreateMap<CarInteriorColor, CarInteriorColorDto>().ReverseMap();
        CreateMap<List<CarInteriorColor>, List<CarInteriorColorDto>>();
        CreateMap<CarTransmission, CarTransmissionDto>().ReverseMap();
        CreateMap<List<CarTransmission>, List<CarTransmissionDto>>();
        CreateMap<CarFuel, CarFuelDto>().ReverseMap();
        CreateMap<List<CarFuel>, List<CarFuelDto>>();
        CreateMap<CarImages, CarImagesDto>().ReverseMap();
        CreateMap<List<CarImages>, List<CarImagesDto>>();
        CreateMap<CarReview, CarReviewDto>().ReverseMap();
        CreateMap<List<CarReview>, List<CarReviewDto>>();
        CreateMap<SavedCar, SavedCarDto>().ReverseMap();
        CreateMap<List<SavedCar>, List<SavedCarDto>>();
        CreateMap<Promotion, PromotionDto>().ReverseMap();
        CreateMap<List<Promotion>, List<PromotionDto>>();
        CreateMap<CompanyProfile, CompanyProfileDto>().ReverseMap();
        CreateMap<List<CompanyProfile>, List<CompanyProfileDto>>();
        CreateMap<ContactMessage, ContactMessageDto>().ReverseMap();
        CreateMap<List<ContactMessage>, List<ContactMessageDto>>();
        CreateMap<Language, LanguageDto>().ReverseMap();
        CreateMap<List<Language>, List<LanguageDto>>();
        CreateMap<UserNotification, UserNotificationDto>().ReverseMap();
        CreateMap<List<UserNotification>, List<UserNotificationDto>>();
        CreateMap<UserImage, UserImageDto>().ReverseMap();
        CreateMap<List<UserImage>, List<UserImageDto>>();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        CreateMap<List<RefreshToken>, List<RefreshTokenDto>>();
        CreateMap<EmailConfirmationToken, EmailConfirmationTokenDto>().ReverseMap();
        CreateMap<List<EmailConfirmationToken>, List<EmailConfirmationTokenDto>>();
        CreateMap<PhoneConfirmationToken, PhoneConfirmationTokenDto>().ReverseMap();
        CreateMap<List<PhoneConfirmationToken>, List<PhoneConfirmationTokenDto>>();
        CreateMap<AppLog, AppLogDto>().ReverseMap();
        CreateMap<List<AppLog>, List<AppLogDto>>();
        CreateMap<UserDevice, UserDeviceDto>().ReverseMap();
        CreateMap<List<UserDevice>, List<UserDeviceDto>>();
        CreateMap<StatePrefix, StatePrefixDto>().ReverseMap();
        CreateMap<List<StatePrefix>, List<StatePrefixDto>>();
    }
}
