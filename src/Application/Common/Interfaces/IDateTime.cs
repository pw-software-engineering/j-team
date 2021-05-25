using System;

namespace HotelReservationSystem.Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}