namespace InjectMe.Tests.TestData
{
    public class Gen<TItem> : IGen<TItem>
    {
        public TItem Item { get; private set; }

        public Gen(TItem item)
        {
            Item = item;
        }
    }
}