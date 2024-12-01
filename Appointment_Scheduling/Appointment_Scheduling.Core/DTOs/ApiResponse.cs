﻿using Newtonsoft.Json;

namespace Appointment_Scheduling.Core.DTOs
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "")
        {
            return new ApiResponse<T> { IsSuccess = true, Message = message, Data = data };
        }

        public static ApiResponse<T> Failure(string message)
        {
            return new ApiResponse<T> { IsSuccess = false, Message = message, Data = default };
        }
    }
}
