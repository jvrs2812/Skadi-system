using System.Collections.Generic;

namespace backend.UseCases
{
    public class ResultResponse<T>
    {
        public ResultResponse(T data,
                              List<String> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultResponse(T data)
        {
            Data = data;
            Errors = new();
        }

        public ResultResponse(List<String> errors)
        {
            Errors = errors;
        }

        public ResultResponse(String errors)
        {
            Errors.Add(errors);
        }
        public T Data { get; private set; }

        public List<String> Errors { get; private set; } = new();
    }
}