using System;
using System.Collections.Generic;
using System.Reflection;

namespace InjectMe.Registration
{
    public interface IAssemblyScanner
    {
        IAssemblyScanner AddTypeFilter(Func<TypeInfo, bool> typeFilter);
        IAssemblyScanner ScanAssemblies(params Assembly[] assemblies);
        IAssemblyScanner ScanAssemblies(IEnumerable<Assembly> assemblies);
        IAssemblyScanner ScanAssemblyContaining<T>();
        IAssemblyScanner UseConvention(IScanConvention convention);
        IAssemblyScanner UseConvention(Func<IScanConvention> conventionDelegate);
        IAssemblyScanner UseConvention<T>() where T : IScanConvention, new();

#if NET45 || WINDOWS_PHONE
        IAssemblyScanner ScanThisAssembly();
        IAssemblyScanner ScanLoadedAssemblies();
        IAssemblyScanner ScanLoadedAssemblies(Func<Assembly, bool> filter);
#endif
    }
}