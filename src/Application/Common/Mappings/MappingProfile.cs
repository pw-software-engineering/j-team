﻿using Application.Hotels;
using Application.Offers;
using Application.Rooms;
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
            CreateMap<Hotel, HotelDto>();
            CreateMap<PreviewHotelFile, byte[]>()
                .ConstructUsing(src => src.Data);
            CreateMap<Offer, OfferDto>();
            CreateMap<PreviewOfferFile, byte[]>()
                .ConstructUsing(src => src.Data);
            CreateMap<Room, RoomDto>();
            //    .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Offers));
            //CreateMap<Offer, int>()
            //    .ConstructUsing(src => src.OfferId);
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