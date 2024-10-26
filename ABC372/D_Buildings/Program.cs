using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 ビル 1, ビル 2, …, ビル N の N 棟のビルがこの順で一列に並んでいます。ビル i (1 ≤ i ≤ N) の高さは H(i) です。
 各 i = 1, 2, …, N について、次を満たす整数 j (i < j ≤ N) の個数を求めてください。

 ・ビル i とビル j の間にビル j より高いビルが存在しない。

 <制約>
 ・1 ≤ N ≤ 2×10^5
 ・1 ≤ H(i) ≤ N
 ・H(i) != H(j) (i != j)
 ・入力はすべて整数

 <入力>
 N
 H(1), H(2), …, H(N)

 <出力>
 各 i = 1, 2, …, N に対して条件を満たす j の個数を c(i) としたとき、 c(1), c(2), …, c(N) をこの順で空白区切りで出力せよ。
 */

namespace D_Buildings {
    internal class Program {
        static void Main(string[] args) {

            var wBuildings = int.Parse(Console.ReadLine());
            var wReversedHeights = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).Reverse().ToList();

            var wAnswers = new List<int>() { 0 };
            var wHeightStack = new Stack<int>();

            for (int i = 1; i < wBuildings; i++) {
                while (wHeightStack.Any() && wHeightStack.Peek() < wReversedHeights[i - 1]) {
                    wHeightStack.Pop();
                }
                wHeightStack.Push(wReversedHeights[i - 1]);
                wAnswers.Add(wHeightStack.Count);
            }
            Console.WriteLine(string.Join(" ", wAnswers.ToArray().Reverse()));
        }
    }
}
