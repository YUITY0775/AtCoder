using System;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc442/tasks/abc442_a
 */

namespace A_Count {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            string text = Console.ReadLine();

            // 出力処理
            Console.WriteLine(CountDots(text));
        }

        /// <summary>
        /// アルファベットの文字列から i と j の数を数える
        /// </summary>
        static int CountDots(string text) {
            if (text == null) return 0;
            return text.Count(x => x == 'i' || x == 'j');
        }
    }
}
