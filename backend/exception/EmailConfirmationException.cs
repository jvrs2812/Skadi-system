namespace backend.exception
{
    public class EmailConfirmationException : Exception
    {
        public EmailConfirmationException(string message) : base(message) { }
    }
}