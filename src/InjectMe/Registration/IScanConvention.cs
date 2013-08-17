using System.Reflection;

namespace InjectMe.Registration
{
    public interface IScanConvention
    {
        void ProcessType(IContainerConfiguration container, TypeInfo type);
    }
}