using System;

//<問題文>
//1 以上 N 以下の整数のうち、10 進法での各桁の和が A 以上 B 以下であるものの総和を求めてください。

//<制約>
//1≤N≤10^4 
//1≤A≤B≤36
//入力はすべて整数である

//<入力>
//N A B

namespace ABC083B___Some_Sums {
    internal class Program {
        static void Main(string[] args) {

            var wInputNumbers = (Console.ReadLine().Split(' '));
            int wNumber = int.Parse(wInputNumbers[0]);
            int wMinNumber = int.Parse(wInputNumbers[1]);
            int wMaxNumber = int.Parse(wInputNumbers[2]);

            int wResult = 0;
            for (int i = 1; i <= wNumber; i++) {
                int wSumOfDigit = GetSumOfDigit(i);
                if (wSumOfDigit < wMinNumber || wMaxNumber < wSumOfDigit) continue;
                wResult += i;
            }
            Console.WriteLine(wResult);
        }

        private static int GetSumOfDigit(int vNumber) {
            int wSum = 0;
            while (0 < vNumber) {
                wSum += vNumber % 10;
                vNumber /= 10;
            }
            return wSum;
        }
    }
}