using System;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc440/tasks/abc440_a
 */

namespace A_Octave {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // 出力処理
            Console.WriteLine(inputs[0] * Math.Pow(2, inputs[1]));
        }
    }
}
