using System;

namespace InjectMe
{
    public class HttpSessionNotSetException : Exception
    {
        public HttpSessionNotSetException()
            : base("The current HttpContext does not have a session.")
        {
        }
    }
}