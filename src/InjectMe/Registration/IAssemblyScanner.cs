using System;
using System.Reflection;

namespace InjectMe.Registration
{
    public interface IAssemblyScanner
    {
        IAssemblyScanner AddAssemblyFilter(Func<Assembly, bool> assemblyFilter);
        IAssemblyScanner AddTypeFilter(Func<TypeInfo, bool> typeFilter);
        IAssemblyScanner ScanAssembly(Assembly assembly);
        IAssemblyScanner ScanAssemblyContaining<T>();
        IAssemblyScanner UseConvention(IScanConvention convention);
        IAssemblyScanner UseConvention(Func<IScanConvention> conventionDelegate);
        IAssemblyScanner UseConvention<T>() where T : IScanConvention, new();

#if NET45 || WINDOWS_PHONE
        IAssemblyScanner ScanThisAssembly();
        IAssemblyScanner ScanLoadedAssemblies();
#endif
    }
}