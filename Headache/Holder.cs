namespace Headache
{
    internal class Holder<T>
    {
        public Holder(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }

    internal static class Holder
    {
        public static Holder<T> Create<T>(T value)
        {
            return new Holder<T>(value);
        }
    }
}
