using System;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc454/tasks/abc454_a
 */

namespace A_ClosedInterval {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            (int left, int right) = (inputs[0], inputs[1]);

            // 出力処理
            Console.WriteLine(right - left + 1);
        }
    }
}
