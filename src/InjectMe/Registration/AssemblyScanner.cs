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
        private static readonly string[] FilteredAssemblies =
        {
            "InjectMe",
            "Microsoft® .NET Framework"
        };

        private readonly IContainerConfiguration _configuration;
        private readonly IList<Func<TypeInfo, bool>> _typeFilters = new List<Func<TypeInfo, bool>>();
        private readonly IList<IScanConvention> _conventions = new List<IScanConvention>();

        public AssemblyScanner(IContainerConfiguration configuration)
        {
            _configuration = configuration;

            AddTypeFilter(type =>
                !IsAnonymousType(type) &&
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

        public IAssemblyScanner AddTypeFilter(Func<TypeInfo, bool> typeFilter)
        {
            _typeFilters.Add(typeFilter);
            return this;
        }

        public IAssemblyScanner ScanAssemblies(IEnumerable<Assembly> assemblies)
        {
            var types =
                from assembly in assemblies
                where IsValidForScanning(assembly)
                from type in assembly.DefinedTypes
                where IsValidForScanning(type)
                select type;

            foreach (var type in types)
            {
                foreach (var convention in _conventions)
                {
                    convention.ProcessType(_configuration, type);
                }
            }

            return this;
        }

        public IAssemblyScanner ScanAssemblies(params Assembly[] assemblies)
        {
            return ScanAssemblies((IEnumerable<Assembly>)assemblies);
        }

        public IAssemblyScanner ScanAssemblyContaining<T>()
        {
            var type = typeof(T);
            var typeInfo = type.GetTypeInfo();
            var assembly = typeInfo.Assembly;

            return ScanAssemblies(assembly);
        }

#if NET45 || WINDOWS_PHONE
        [MethodImpl(MethodImplOptions.NoInlining)]
        public IAssemblyScanner ScanThisAssembly()
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            return ScanAssemblies(callingAssembly);
        }

        public IAssemblyScanner ScanLoadedAssemblies()
        {
            var assemblies = GetLoadedAssemblies();

            ScanAssemblies(assemblies);

            return this;
        }

        public IAssemblyScanner ScanLoadedAssemblies(Func<Assembly, bool> filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter");

            var assemblies = from assembly in GetLoadedAssemblies()
                             where filter(assembly)
                             select assembly;

            ScanAssemblies(assemblies);

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

        private bool IsValidForScanning(Assembly assembly)
        {
            var productAttribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            if (productAttribute != null)
            {
                if (FilteredAssemblies.Contains(productAttribute.Product))
                    return false;
            }

            return true;
        }

        private bool IsValidForScanning(TypeInfo type)
        {
            return _typeFilters.All(filter => filter(type));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasPreventionAttribute(TypeInfo type)
        {
            return type.IsDefined(PreventAutomaticRegistrationAttributeType);
        }
    }
}