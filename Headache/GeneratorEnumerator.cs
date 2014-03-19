using System.Collections.Generic;

namespace Headache
{
    internal class GeneratorEnumeratorResult<T>
    {
        public static GeneratorEnumeratorResult<T> WithValue(T value)
        {
            return new GeneratorEnumeratorResult<T>(value, false);
        }

        public static GeneratorEnumeratorResult<T> WithNoValue()
        {
            return new GeneratorEnumeratorResult<T>(default(T), true);
        }

        private GeneratorEnumeratorResult(T value, bool done)
        {
            Value = value;
            Done = done;
        }

        public T Value { get; private set; }
        public bool Done { get; private set; }

        public override string ToString()
        {
            return (Done)
                       ? string.Format("{{value: undefined, done: true}}")
                       : string.Format("{{value: {0}, done: false}}", Value);
        }
    }

    internal class GeneratorEnumerator<T1, T2>
    {
        private readonly IEnumerator<T1> _enumerator;
        private readonly Holder<T2> _holder;

        public GeneratorEnumerator(IEnumerator<T1> enumerator, Holder<T2> holder)
        {
            _enumerator = enumerator;
            _holder = holder;
        }

        public GeneratorEnumeratorResult<T1> Next()
        {
            return _enumerator.MoveNext()
                       ? GeneratorEnumeratorResult<T1>.WithValue(_enumerator.Current)
                       : GeneratorEnumeratorResult<T1>.WithNoValue();
        }

        public GeneratorEnumeratorResult<T1> Next(T2 value)
        {
            _holder.Value = value;
            return Next();
        }
    }
}
