using InjectMe.Web;

// ReSharper disable once CheckNamespace
namespace InjectMe.Registration
{
    public static class AssemblyScannerExtensions
    {
        public static IAssemblyScanner RegisterControllers(this IAssemblyScanner scanner)
        {
            return scanner.UseConvention<ControllerScanConvention>();
        }
    }
}