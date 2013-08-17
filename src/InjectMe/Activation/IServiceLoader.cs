namespace InjectMe.Activation
{
    public interface IServiceLoader<out T>
    {
        T LoadService();
    }
}