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
            var assemblies = BuildManager.
                GetReferencedAssemblies().
                Cast<Assembly>();

            scanner.ScanAssemblies(assemblies);

            return scanner;
        }
    }
}