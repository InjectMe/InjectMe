namespace InjectMe.Tests.TestData
{
    public interface IGen<out T>
    {
        T Item { get; }
    }

    public interface IGen<out T1, out T2>
    {
        T1 Item1 { get; }
        T2 Item2 { get; }
    }
}