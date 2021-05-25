using HotelReservationSystem.Application.Common.Interfaces;
using System;

namespace HotelReservationSystem.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
