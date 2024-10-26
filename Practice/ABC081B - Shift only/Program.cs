using System;
using System.Linq;

namespace ABC081B___Shift_only {
    internal class Program {
        static void Main(string[] args) {

            int wNumCount = int.Parse(Console.ReadLine());
            var wNumbers = (Console.ReadLine().Split(' ')).Select(int.Parse).ToArray();

            if (wNumCount != wNumbers.Count()) {
                Console.WriteLine("入力値が不正です。");
            }

            var wResult = GetResult(wNumbers);

            Console.WriteLine(wResult);
        }

        private static int GetResult(int[] vNumbers) {

            int wCount = 0;
            while (vNumbers.All(x => x % 2 == 0)) {

                vNumbers = vNumbers.Select(x => x / 2).ToArray();
                wCount++;
            }
            return wCount;
        }
    }
}
