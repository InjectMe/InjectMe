using InjectMe.Registration;

namespace InjectMe.WebApi
{
    public static class AssemblyScannerExtensions
    {
        public static IAssemblyScanner RegisterApiControllers(this IAssemblyScanner scanner)
        {
            return scanner.UseConvention<ApiControllerScanConvention>();
        }
    }
}