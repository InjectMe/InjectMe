using InjectMe.Registration;

namespace InjectMe.Web
{
    public static class AssemblyScannerExtensions
    {
        public static IAssemblyScanner RegisterControllers(this IAssemblyScanner scanner)
        {
            return scanner.UseConvention<ControllerScanConvention>();
        }
    }
}