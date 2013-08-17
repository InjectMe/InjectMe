namespace InjectMe.Registration
{
    public static class ServiceLoaderScanConventionExtensions
    {
        public static IAssemblyScanner RegisterServiceLoaders(this IAssemblyScanner assemblyScanner)
        {
            return assemblyScanner.UseConvention<ServiceLoaderScanConvention>();
        }
    }
}