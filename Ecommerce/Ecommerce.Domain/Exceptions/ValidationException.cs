namespace Ecommerce.Domain.Exceptions
{
    public class ValidationException(IDictionary<string, string[]> errors) : Exception("Um ou mais campos estão inválidos.")
    {
        public IDictionary<string, string[]> Errors { get; } = errors;
    }
}
