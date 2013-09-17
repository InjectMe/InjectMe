namespace InjectMe.Registration
{
    public static class DefaultScanConventionsExtensions
    {
        public static IAssemblyScanner UseDefaultConventions(this IAssemblyScanner scanner, bool usePrefixResolution = true)
        {
            var convention = new DefaultScanConvention(usePrefixResolution);

            return scanner.UseConvention(convention);
        }
    }
}