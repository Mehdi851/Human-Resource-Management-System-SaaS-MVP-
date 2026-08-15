using HRMS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Application.Common.Helpers
{
    public static class ResponseFactory
    {
        public static ApiResponse<T> Success<T>(
            T data,
            string message = "")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Failure<T>(
            string message,
            params string[] errors)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors.ToList()
            };
        }
    }
}
