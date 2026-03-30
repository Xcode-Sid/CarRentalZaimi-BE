using AutoMapper;
using CarRentalZaimi.Application.DTOs;
using CarRentalZaimi.Domain.Entities;

namespace CarRentalZaimi.Application.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Car, CarDto>();
    }
}