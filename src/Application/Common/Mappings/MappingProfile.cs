using Application.Hotels;
using Application.Offers;
using Application.Rooms;
using Application.Reservations;
using AutoMapper;
using HotelReservationSystem.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace HotelReservationSystem.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.HotelPreviewPictureData, opt => opt.MapFrom(src => src.HotelPreviewPicture))
                .ForMember(dest => dest.PicturesData, opt => opt.MapFrom(src => src.Pictures))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Hotel, HotelListedDto>()
                .ForMember(dest => dest.HotelPreviewPictureData, opt => opt.MapFrom(src => src.HotelPreviewPicture))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name));
            CreateMap<File, byte[]>()
                .ConstructUsing(src => src.Data);
            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.OfferPreviewPictureData, opt => opt.MapFrom(src => src.OfferPreviewPicture))
                .ForMember(dest => dest.PicturesData, opt => opt.MapFrom(src => src.Pictures));
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.OffersData, opt => opt.MapFrom(src => src.Offers))
                .ForMember(dest => dest.OfferID, opt => opt.MapFrom(src => src.Offers));
            CreateMap<Offer, int>()
                .ConstructUsing(src => src.OfferId);
            CreateMap<Reservation, ReservationDto>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Client.Name))
              .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Client.Surname))
              .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Client.Username))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Client.Email));
        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}