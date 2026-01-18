using System;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc431/tasks/abc431_a
 */

namespace A_RobotBalance {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            (int headWeight, int bodyWeight) = (inputs[0], inputs[1]);

            // 出力処理
            Console.WriteLine(headWeight <= bodyWeight ? 0 : headWeight - bodyWeight);
        }
    }
}
