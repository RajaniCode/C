using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Concurrency
{
    class Program
    {
        static void Main(string[] args)
        {

            DateTime t1 = DateTime.Now;
            PrintPrimaryNumbers();
            var ts1 = DateTime.Now.Subtract(t1);
            Console.WriteLine("Finished Sync and started Async");
            var t2 = DateTime.Now;
            PrintPrimaryNumbersAsync();
            var ts2 = DateTime.Now.Subtract(t2);

            Console.WriteLine(string.Format("It took {0} for the sync call and {1} for the Async one", ts1, ts2));
            Console.WriteLine("Any Key to terminate!!");
            Console.ReadLine();
        }

        private static async void PrintPrimaryNumbersAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                var result = await getPrimesAsync(i + 1, i * 10);
                result.ToList().ForEach(x => Console.WriteLine(string.Format("This is generated async {0}", x)));
            }
        }
        private static void PrintPrimaryNumbers()
        {
            for (int i = 0; i < 10; i++)
                getPrimes(i + 1, i * 10)
                    .ToList().
                    ForEach(x => Console.WriteLine(string.Format("This is generated sync {0}", x)));
        }
        public static int getPrimeCount(int min, int count)
        {
            return ParallelEnumerable.Range(min, count).Count(n=> 
                Enumerable.Range(2,(int)Math.Sqrt(n)-1).All(i=>
                n%i>0));
        }
        public static IEnumerable<int> getPrimes(int min, int count)
        {
            return Enumerable.Range(min, count).Where
              (n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i =>
                n % i > 0));
        }
        public static Task<IEnumerable<int>> getPrimesAsync(int min, int count)
        {
             return Task.Run (()=> Enumerable.Range(min, count).Where
              (n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i =>
                n % i > 0)));
        }

    }
}
