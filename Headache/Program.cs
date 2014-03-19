using System;
using System.Collections.Generic;

// http://www.sitepoint.com/javascript-generators-preventing-callback-hell/

namespace Headache
{
    internal class Program
    {
        private const string FileName1 = @"C:\Users\taylojo\Documents\file1.dat";
        private const string FileName2 = @"C:\Users\taylojo\Documents\file2.dat";

        private static void Main()
        {
            foreach (var v in HelloGen())
            {
                Console.WriteLine("v = {0}", v);
            }
            Console.WriteLine();

            var holder = Holder.Create(0);
            var e = HelloGen2(holder).GetEnumerator();
            var ge = new GeneratorEnumerator<int, int>(e, holder);
            Console.WriteLine("next() = {0}", ge.Next());
            Console.WriteLine("next(500) = {0}", ge.Next(500));
            Console.WriteLine("next(1000) = {0}", ge.Next(1000));
            Console.WriteLine();

            var co = new Co<Byte[]>(ThunkGen);

            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
        }

        private static IEnumerable<Action<Action<Exception, Byte[]>>> ThunkGen(Holder<Byte[]> holder)
        {
            yield return Thunks.ReadFile(FileName1);
            var file1 = holder.Value;
            yield return Thunks.ReadFile(FileName2);
            var file2 = holder.Value;

            Console.WriteLine("file1.Length: {0,15:N0}", file1.Length);
            Console.WriteLine("file2.Length: {0,15:N0}", file2.Length);
        }

        private static IEnumerable<int> HelloGen()
        {
            yield return 100;
            yield return 400;
        }

        private static IEnumerable<int> HelloGen2(Holder<int> holder)
        {
            yield return 100;
            var a = holder.Value;
            yield return a + 100;
            var b = holder.Value;
            Console.WriteLine("b = {0}", b);
        }
    }
}
