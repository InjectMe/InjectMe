using InjectMe.WebApi;

// ReSharper disable once CheckNamespace
namespace InjectMe.Registration
{
    public static class AssemblyScannerExtensions
    {
        public static IAssemblyScanner RegisterApiControllers(this IAssemblyScanner scanner)
        {
            return scanner.UseConvention<ApiControllerScanConvention>();
        }
    }
}