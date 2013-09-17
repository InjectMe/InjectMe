using System;

namespace InjectMe
{
    public class UnboundTypeActivationFailedException : Exception
    {
        public Type UnboundType { get; private set; }

        public UnboundTypeActivationFailedException(Type unboundType)
            : base(CreateMessage(unboundType))
        {
            UnboundType = unboundType;
        }

        private static string CreateMessage(Type unboundType)
        {
            return string.Format("The unbound type '{0}' can't be activated.", unboundType.Name);
        }
    }
}