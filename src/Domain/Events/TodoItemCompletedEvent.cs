using HotelReservationSystem.Domain.Common;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
