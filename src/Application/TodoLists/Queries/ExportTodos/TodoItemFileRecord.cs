using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
