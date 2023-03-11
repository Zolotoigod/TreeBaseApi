using System.ComponentModel;

namespace TreeBase.ExceptionHandling
{
    public class SecureException : Exception
    {
        public SecureException(string message) : base(message)
        {
        }
    }
}
