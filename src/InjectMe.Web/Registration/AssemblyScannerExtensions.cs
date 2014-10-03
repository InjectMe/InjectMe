using System;
using System.Collections.Generic;
using InjectMe.Web;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

// ReSharper disable once CheckNamespace
namespace InjectMe.Registration
{
    public static class AssemblyScannerExtensions
    {
        public static IAssemblyScanner RegisterControllers(this IAssemblyScanner scanner)
        {
            return scanner.UseConvention<ControllerScanConvention>();
        }

        public static IAssemblyScanner ScanReferencedAssemblies(this IAssemblyScanner scanner)
        {
            var assemblies = GetReferencedAssemblies();

            scanner.ScanAssemblies(assemblies);

            return scanner;
        }

        public static IAssemblyScanner ScanReferencedAssemblies(this IAssemblyScanner scanner, Func<Assembly, bool> filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter");

            var assemblies =
                GetReferencedAssemblies().
                Where(filter);

            scanner.ScanAssemblies(assemblies);

            return scanner;
        }

        private static IEnumerable<Assembly> GetReferencedAssemblies()
        {
            return BuildManager.
                GetReferencedAssemblies().
                Cast<Assembly>();
        }
    }
}