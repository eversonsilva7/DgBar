using System;

namespace BarDoDG.Application.Validation
{
    public sealed class ObjectNotFoundException : Exception
    {
        public string ErrorCode { get; set; }

        public ObjectNotFoundException() { }

        public ObjectNotFoundException(string Message) : base(Message) { }

        public ObjectNotFoundException(string Message, string errorCode) : base(Message)
        {
            this.ErrorCode = errorCode;
        }

        public ObjectNotFoundException(string message, Exception ex) : base(message, ex) { }

        public override string ToString()
        {
            return ($"Mensagem: '{Message}' - Código: '{ErrorCode}'");
        }
    }
}
