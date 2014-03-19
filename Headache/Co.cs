using System;
using System.Collections.Generic;

namespace Headache
{
    internal class Co
    {
        private readonly IEnumerable<Action<Thunks.ReadFileCallback>> _gen;
        private readonly Holder<byte[]> _holder;
        private readonly GeneratorEnumerator<Action<Thunks.ReadFileCallback>, byte[]> _ge;

        public Co(Func<Holder<byte[]>, IEnumerable<Action<Thunks.ReadFileCallback>>> generator)
        {
            _holder = Holder.Create(null as Byte[]);
            _gen = generator(_holder);
            _ge = new GeneratorEnumerator<Action<Thunks.ReadFileCallback>, Byte[]>(_gen.GetEnumerator(), _holder);

            NextItem();
        }

        private void NextItem()
        {
            var item = _ge.Next();
            if (!item.Done)
            {
                item.Value(NextItem);
            }
        }

        private void NextItem(Exception err, Byte[] result)
        {
            var item = _ge.Next(result);
            if (!item.Done)
            {
                item.Value(NextItem);
            }
        }
    }
}
