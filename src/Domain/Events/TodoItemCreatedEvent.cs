using HotelReservationSystem.Domain.Common;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
