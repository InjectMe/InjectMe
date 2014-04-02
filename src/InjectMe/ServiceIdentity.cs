using System;
using System.Diagnostics;

namespace InjectMe
{
    [DebuggerDisplay("{ServiceType.FullName,nq}{ServiceName != null ? \", Name = \\\"\" + ServiceName + \"\\\"\" : \"\", nq}")]
    public class ServiceIdentity
    {
        private readonly int _hashCode;

        public Type ServiceType { get; private set; }
        public string ServiceName { get; private set; }

        public ServiceIdentity(Type serviceType, string serviceName = null)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            ServiceType = serviceType;
            ServiceName = serviceName;

            _hashCode = CalculateHashCode();
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ServiceIdentity;
            if (other == null)
                return false;

            return
                ServiceType == other.ServiceType &&
                string.Equals(ServiceName, other.ServiceName, StringComparison.OrdinalIgnoreCase);
        }

        private int CalculateHashCode()
        {
            var hashCode = ServiceType.GetHashCode();

            if (ServiceName != null)
                hashCode ^= ServiceName.ToLowerInvariant().GetHashCode();

            return hashCode;
        }
    }
}