namespace backend.exception
{
    public class ValidateException : Exception
    {
        public ValidateException(string message) : base(message) { }
    }
}