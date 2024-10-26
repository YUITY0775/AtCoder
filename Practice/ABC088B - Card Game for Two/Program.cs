using System;
using System.Collections.Generic;
using System.Linq;

//<問題文>
//N 枚のカードがあります。 i 枚目のカードには,a(i)という数が書かれています。
//Alice と Bob は、これらのカードを使ってゲームを行います。ゲームでは, Alice と Bob が
//交互に 1 枚ずつカードを取っていきます。Alice が先にカードを取ります。
//2 人がすべてのカードを取ったときゲームは終了し、取ったカードの数の合計がその人の得点になります。
//2 人とも自分の得点を最大化するように最適な戦略を取った時、Alice は Bob より何点多く取るか求めてください。

//<制約>
//N は 1 以上 100 以下の整数
//a(i) (1≤i≤N) は 1 以上 100 以下の整数

//<入力>
//N
//a(1) a(2) a(3) … a(N)

namespace ABC088B___Card_Game_for_Two {
    internal class Program {
        static void Main(string[] args) {

            int wNumCount = int.Parse(Console.ReadLine());
            var wNumbers = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();

            var wResult = GetScore(wNumbers, true) - GetScore(wNumbers, false);

            Console.WriteLine(wResult);
        }

        public static int GetScore(IReadOnlyList<int> vNumbers, bool vIsAlice) {

            var wOrderedNumbers = vNumbers.OrderByDescending(x => x).ToList();

            int wScore = 0;
            for (int i = vIsAlice ? 0 : 1; i < wOrderedNumbers.Count; i += 2) {
                wScore += wOrderedNumbers[i];
            }
            return wScore;
        }
    }
}