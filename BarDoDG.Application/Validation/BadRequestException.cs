namespace BarDoDG.Application.Validation
{
    public sealed class BadRequestException : System.Exception
    {
        public BadRequestException() : base("Valor obrigatório.") { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
