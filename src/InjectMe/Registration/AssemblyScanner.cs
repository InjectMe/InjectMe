using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace InjectMe.Registration
{
    public partial class AssemblyScanner : IAssemblyScanner
    {
        private static readonly Type PreventAutomaticRegistrationAttributeType = typeof(PreventAutomaticRegistrationAttribute);

        private readonly IContainerConfiguration _configuration;
        private readonly IList<Func<Assembly, bool>> _assemblyFilters = new List<Func<Assembly, bool>>();
        private readonly IList<Func<TypeInfo, bool>> _typeFilters = new List<Func<TypeInfo, bool>>();
        private readonly IList<IScanConvention> _conventions = new List<IScanConvention>();

        public AssemblyScanner(IContainerConfiguration configuration)
        {
            _configuration = configuration;

            AddTypeFilter(type =>
                !IsAnonymousType(type) &&
                !IsManuallyRegisteredType(type) &&
                !HasPreventionAttribute(type)
            );
        }

        public IAssemblyScanner UseConvention(IScanConvention convention)
        {
            _conventions.Add(convention);
            return this;
        }

        public IAssemblyScanner UseConvention(Func<IScanConvention> conventionDelegate)
        {
            var convention = conventionDelegate();
            return UseConvention(convention);
        }

        public IAssemblyScanner UseConvention<T>() where T : IScanConvention, new()
        {
            var convention = new T();
            return UseConvention(convention);
        }

        public IAssemblyScanner AddAssemblyFilter(Func<Assembly, bool> assemblyFilter)
        {
            _assemblyFilters.Add(assemblyFilter);
            return this;
        }

        public IAssemblyScanner AddTypeFilter(Func<TypeInfo, bool> typeFilter)
        {
            _typeFilters.Add(typeFilter);
            return this;
        }

        public IAssemblyScanner ScanAssembly(Assembly assembly)
        {
            var types = assembly.DefinedTypes;

            foreach (var typeFilter in _typeFilters)
            {
                types = types.Where(typeFilter);
            }

            foreach (var type in types)
            {
                foreach (var convention in _conventions)
                {
                    convention.ProcessType(_configuration, type);
                }
            }

            return this;
        }

        public IAssemblyScanner ScanAssemblyContaining<T>()
        {
            var type = typeof(T);
            var typeInfo = type.GetTypeInfo();
            var assembly = typeInfo.Assembly;

            return ScanAssembly(assembly);
        }

#if NET45 || WINDOWS_PHONE
        [MethodImpl(MethodImplOptions.NoInlining)]
        public IAssemblyScanner ScanThisAssembly()
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            return ScanAssembly(callingAssembly);
        }

        public IAssemblyScanner ScanLoadedAssemblies()
        {
            var assemblies = GetLoadedAssemblies();

            foreach (var assemblyFilter in _assemblyFilters)
            {
                assemblies = assemblies.Where(assemblyFilter);
            }

            foreach (var assembly in assemblies)
            {
                ScanAssembly(assembly);
            }

            return this;
        }

        private static IEnumerable<Assembly> GetLoadedAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsAnonymousType(TypeInfo type)
        {
            return type.Name.StartsWith("<>");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsManuallyRegisteredType(TypeInfo type)
        {
            return
                type.Namespace != null &&
                type.Namespace.StartsWith("InjectMe.DI");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasPreventionAttribute(TypeInfo type)
        {
            return type.IsDefined(PreventAutomaticRegistrationAttributeType);
        }
    }
}