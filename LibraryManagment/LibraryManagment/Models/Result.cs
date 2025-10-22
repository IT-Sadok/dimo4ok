using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Models
{
    public class Result
    {
        public bool IsSuccess { get; init; } = true;
        public string Message { get; init; } = null!;

        public static Result Success(string message) => new Result { Message = message};
        public static Result Fail(string message) => new Result {IsSuccess = false, Message = message };
    }

    public class Result<T> : Result
    {
        public T? Data { get; init; }

        public static Result<T> Success(string message, T data) => new Result<T> {Message = message ,Data = data };
        new public static Result<T> Fail(string message) => new Result<T> {IsSuccess = false, Message = message};
    }
}
