

namespace Tamplate.Application.Exceptions
{
    public class TamplateException : Exception
    {
        protected TamplateException(string message) : base(message) { }
    }
    public class TamplateValidationException : TamplateException
    {
        public TamplateValidationException(string message) : base(message) { }
    }
}
