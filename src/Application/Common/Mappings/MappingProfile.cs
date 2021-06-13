using Application.Hotels;
using Application.Offers;
using Application.Rooms;
using Application.Reservations;
using AutoMapper;
using HotelReservationSystem.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;
using Application.Reviews;

namespace HotelReservationSystem.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<Hotel, HotelDto>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(dest => dest.HotelPreviewPictureData, opt => opt.MapFrom(src => src.HotelPreviewPicture))
                .ForMember(dest => dest.HotelPicturesData, opt => opt.MapFrom(src => src.Pictures))
                .ForMember(dest => dest.HotelDesc, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Hotel, HotelListedDto>()
                .ForMember(dest => dest.HotelPreviewPictureData, opt => opt.MapFrom(src => src.HotelPreviewPicture))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name));
            CreateMap<File, byte[]>()
                .ConstructUsing(src => src.Data);
            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.OfferID, opt => opt.MapFrom(src => src.OfferId))
                .ForMember(dest => dest.OfferTitle, opt => opt.MapFrom(src => src.Title))
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
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Client.Email))
                .ForMember(dest => dest.HotelRoomNumber, opt => opt.MapFrom(s => s.Room.HotelRoomNumber));
            CreateMap<Review, ReviewDto>()
                .ForMember(d => d.OfferID, o => o.MapFrom(s => s.OfferId))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ReviewId));
            CreateMap<Reservation, ClientReservationResult>()
                .ForMember(dest => dest.reservationInfo, o => o.MapFrom(s => s))
                .ForMember(dest => dest.hotelInfoPreview, o => o.MapFrom(s => s.Offer.Hotel))
                .ForMember(dest => dest.offerInfoPreview, o => o.MapFrom(s => s.Offer));
            CreateMap<Reservation, ReservationInfo>()
                .ForMember(dest => dest.reservationID, opt => opt.MapFrom(o => o.ReservationId))
                .ForMember(dest => dest.from, opt => opt.MapFrom(o => o.FromTime))
                .ForMember(dest => dest.to, opt => opt.MapFrom(o => o.ToTime))
                .ForMember(dest => dest.numberOfAdults, opt => opt.MapFrom(o => o.AdultsCount))
                .ForMember(dest => dest.numberOfChildren, opt => opt.MapFrom(o => o.ChildrenCount))
                .ForMember(dest => dest.reviewID,opt => opt.MapFrom(o => 0));
            CreateMap<Hotel, HotelInfoPreview>()
                .ForMember(dest => dest.city, o => o.MapFrom(s => s.City))
                .ForMember(dest => dest.country, o => o.MapFrom(s => s.Country))
                .ForMember(dest => dest.hotelName, o => o.MapFrom(s => s.Name))
                .ForMember(dest => dest.hotelID, o => o.MapFrom(s => s.HotelId));
            CreateMap<Offer, OfferInfoPreview>()
                .ForMember(dest => dest.offerTitle, o => o.MapFrom(s => s.Title))
                .ForMember(dest => dest.offerID, o => o.MapFrom(s => s.OfferId))
                .ForMember(dest => dest.offerPreviewPicture, o => o.MapFrom(s => s.OfferPreviewPicture.Data));
            CreateMap<HotelDto, HotelDetailsDto>()
            .ForMember(d => d.HotelDescription, o => o.MapFrom(s => s.HotelDesc));
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