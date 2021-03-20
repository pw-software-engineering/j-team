using HotelReservationSystem.Application.Common.Mappings;
using HotelReservationSystem.Domain.Entities;
using System.Collections.Generic;

namespace HotelReservationSystem.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoList>
    {
        public TodoListDto()
        {
            Items = new List<TodoItemDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItemDto> Items { get; set; }
    }
}
