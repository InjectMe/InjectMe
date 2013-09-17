using System;

namespace InjectMe
{
    public class UnboundTypeConstructionFailedException : Exception
    {
        public Type UnboundType { get; private set; }

        public UnboundTypeConstructionFailedException(Type unboundType)
            : base(CreateMessage(unboundType))
        {
            UnboundType = unboundType;
        }

        private static string CreateMessage(Type unboundType)
        {
            return string.Format("The unbound type '{0}' can't be constructed.", unboundType.Name);
        }
    }
}