using System;

namespace InjectMe
{
    public class HttpContextNotSetException : Exception
    {
        public HttpContextNotSetException()
            : base("Could not get current HttpContext.")
        {
        }
    }
}