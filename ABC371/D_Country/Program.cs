using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 数直線上に N 個の村があります。i 番目の村は座標 X(i) にあり、P(i) 人の村人がいます。
 Q 個のクエリに答えてください。i 番目のクエリは以下の形式です。

 ・整数 L(i), R(i) が与えられる。座標が L(i) 以上 R(i) 以下の村に住んでいる村人の人数の総数を求めよ。

 <制約>
 ・1 ≤ N, Q ≤ 2×10^5
 ・−10^9 ≤ X(1) < X(2) < … < X(N) ≤ 10^9 
 ・1 ≤ P(i) ≤ 10^9
 ・−10^9 ≤ L(i) ≤ R(i) ≤ 10^9
 ・入力される数値は全て整数

 <入力>
 N
 X(1) … X(N)​ 
 P(1) … P(N)
 Q
 L(1) R(1) 
 …
 L(Q) R(Q)

 <出力>
 Q 行出力せよ。
 i (1 ≤ i ≤ Q) 行目には、i 番目のクエリに対する答えを出力せよ。
 */

namespace D_Country {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wVillageCount = int.Parse(Console.ReadLine());
            var wVillagePositions = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var wVillagePopulations = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            var wVillageInfos = new Dictionary<int, int>();
            for (int i = 0; i < wVillageCount; i++) {
                wVillageInfos.Add(wVillagePositions[i], wVillagePopulations[i]);
            }

            var wQueries = new List<int[]>();
            for (int i = 0; i < int.Parse(Console.ReadLine()); i++) {
                var wLine = Console.ReadLine().Split(' ');
                wQueries.Add(new int[] { int.Parse(wLine[0]), int.Parse(wLine[1]) });
            }

            // 座標 -10^9 から X(i) までにある村の住民の人数の累積和を考える
            var wRunningTotal = 
        }
    }
}
