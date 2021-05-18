using HotelReservationSystem.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebUI
{
    public class ApiResponse<T> : ObjectResult
    {
        public ApiResponse(T data) : base(data)
        {
            this.StatusCode = 400;
        }
        public ApiResponse(T data, int code) : base(data)
        {
            this.StatusCode = code;
        }
        public ApiResponse(string error, int code) : base(new ErrorResponse() { Error = error })
        {
            this.StatusCode = code;
        }
    }
    public class ApiResponse : ObjectResult
    {
        public ApiResponse(string error, int code) : base(new ErrorResponse() { Error = error })
        {
            this.StatusCode = code;
        }
    }
    public class ErrorResponse
    {
        public string Error { get; set; }
    }
}