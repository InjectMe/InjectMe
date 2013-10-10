namespace InjectMe.Tests.TestData
{
    public class Gen<T> : IGen<T>
    {
        public T Item { get; private set; }

        public Gen()
        {
        }

        public Gen(T item)
        {
            Item = item;
        }
    }

    public class Gen<T1, T2> : IGen<T1, T2>
    {
        public T1 Item1 { get; private set; }
        public T2 Item2 { get; private set; }

        public Gen()
        {
        }

        public Gen(T1 item1)
        {
            Item1 = item1;
        }

        public Gen(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}