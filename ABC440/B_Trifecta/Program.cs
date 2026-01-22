using System;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc440/tasks/abc440_b
 */

namespace B_Trifecta {
    internal class Program {
        static void Main(string[] args) {

            const int HOW_MANY_DISPLAY = 3;

            // 入力受付
            _ = Console.ReadLine();

            // タイムと馬の番号をセットで取得
            var horses = Console.ReadLine().Split(' ').Select((x, i) => (Time: long.Parse(x), Index: i + 1)).ToArray();

            // 出力処理
            Console.WriteLine(string.Join(" ", horses.OrderBy(x => x.Time).Select(x => x.Index).Take(HOW_MANY_DISPLAY)));
        }
    }
}
