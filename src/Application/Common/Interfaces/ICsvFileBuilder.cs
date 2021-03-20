using HotelReservationSystem.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace HotelReservationSystem.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
