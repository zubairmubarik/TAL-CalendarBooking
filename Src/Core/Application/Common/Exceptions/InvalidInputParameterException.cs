namespace Application.Common.Exceptions
{
    public class InvalidInputParameterException : Exception
    {
        public InvalidInputParameterException()
        {
        }

        public InvalidInputParameterException(string objectType)
            : base($"Parrameters of type {objectType} are missing or invalid.")
        {
        }

        public InvalidInputParameterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}