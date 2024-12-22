using System;
using System.Linq;

/*
 <問題>
 3 つの整数 A,B,C が与えられます。これら 3 つの整数を 
 2 つ以上のグループに分けて、それぞれのグループ内の数の和を等しくできるかどうか判定してください。

 <制約>
 ・1 ≤ A, B, C ≤ 1000
 ・入力はすべて整数

 <入力>
 A B C

 <出力>
 A,B,C を和の等しい 2 つ以上のグループに分けることができるならば Yes を、できないならば No を出力せよ。
 */

namespace A_Equally {
    internal class Program {
        static void Main(string[] args) {

            var wNumbers = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            Console.WriteLine(CanDivide(wNumbers) ? "Yes" : "No");
        }

        /// <summary>
        /// 長さ 3 の整数配列を和の等しい 2 つ以上のグループに分けることができるか否かを返す
        /// </summary>
        /// <param name="vNumbers">整数配列(長さ 3)</param>
        /// <returns>整数配列を和の等しい 2 つ以上のグループに分けることができるか否か</returns>
        static bool CanDivide(int[] vNumbers) {
            int wTotal = vNumbers.Sum();
            if (wTotal / vNumbers.Length == vNumbers[0]) return true;
            for (int i = 0; i < vNumbers.Length; i++) {
                if (wTotal - vNumbers[i] == vNumbers[i]) return true;
            }
            return false;
        }
    }
}
