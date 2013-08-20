using System;

namespace InjectMe
{
    public class ActivatorGroupAlreadyRegisteredException : Exception
    {
        protected Type ServiceType { get; set; }

        public ActivatorGroupAlreadyRegisteredException(Type serviceType)
            : base(CreateMessage(serviceType))
        {
            ServiceType = serviceType;
        }

        private static string CreateMessage(Type serviceType)
        {
            return string.Format("An activator group for service type '{0}' has already been registered.", serviceType);
        }
    }
}