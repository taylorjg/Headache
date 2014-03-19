using System;
using System.Collections.Generic;

namespace Headache
{
    internal class Co<T>
    {
        private readonly IEnumerable<Action<Action<Exception, T>>> _gen;
        private readonly Holder<T> _holder;
        private readonly GeneratorEnumerator<Action<Action<Exception, T>>, T> _ge;

        public Co(Func<Holder<T>, IEnumerable<Action<Action<Exception, T>>>> generator)
        {
            _holder = Holder.Create(default(T));
            _gen = generator(_holder);
            _ge = new GeneratorEnumerator<Action<Action<Exception, T>>, T>(_gen.GetEnumerator(), _holder);

            NextItem();
        }

        private void NextItem()
        {
            HandleItem(_ge.Next());
        }

        private void NextItem(Exception ex, T result)
        {
            HandleItem(_ge.Next(result));
        }

        private void HandleItem(GeneratorEnumeratorResult<Action<Action<Exception, T>>> item)
        {
            if (!item.Done)
            {
                item.Value(NextItem);
            }
        }
    }
}
