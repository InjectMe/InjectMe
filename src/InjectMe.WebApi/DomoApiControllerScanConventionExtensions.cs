using InjectMe.Registration;

namespace InjectMe.WebApi
{
    public static class DomoApiControllerScanConventionExtensions
    {
        public static IAssemblyScanner UseApiControllerConventions(this IAssemblyScanner assemblyScanner)
        {
            return assemblyScanner.UseConvention<DomoApiControllerScanConvention>();
        }
    }
}