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
            var wVillagePoints = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var wPopulations = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            var wQueries = new List<int[]>();
            int wQueryCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < wQueryCount; i++) {
                var wLine = Console.ReadLine().Split(' ');
                wQueries.Add(new int[] { int.Parse(wLine[0]), int.Parse(wLine[1]) });
            }

            // i 番目の村までの住民の人数の累積和を考える
            var wRunningTotal = new List<long>();
            long wPopulation = 0;
            for (int i = 0; i < wVillageCount; i++) {
                wPopulation += wPopulations[i];
                wRunningTotal.Add(wPopulation);
            }

            foreach (int[] wQuery in wQueries) {
                int wRightIndex = GetVillageIndex(wQuery[1], wVillagePoints);
                int wLeftIndex = GetVillageIndex(wQuery[0] - 1, wVillagePoints);
                long wAnswer = (wRightIndex < 0 ? 0 : wRunningTotal[wRightIndex]) - (wLeftIndex < 0 ? 0 : wRunningTotal[wLeftIndex]);
                Console.WriteLine(wAnswer);
            }
        }

        /// <summary>
        /// 対象座標とすべての村の座標配列を受け取り、対象座標以下の最右端にある村が何番目の村かを返す。
        /// </summary>
        /// <param name="vPoint">検索対象座標</param>
        /// <param name="vVillagePoints">すべての村の座標配列</param>
        /// <returns>対象座標以下の最右端にある村のインデックス</returns>
        static int GetVillageIndex(int vPoint, int[] vVillagePoints) {
            int wLeftIndex = 0;
            int wRightIndex = vVillagePoints.Length - 1;

            while (wRightIndex - wLeftIndex > 1) {
                int wMidIndex = (wLeftIndex + wRightIndex) / 2;
                if (vPoint == vVillagePoints[wMidIndex]) {
                    return wMidIndex;
                } else if (vPoint < vVillagePoints[wMidIndex]) {
                    wRightIndex = wMidIndex;
                } else {
                    wLeftIndex = wMidIndex;
                }
            }
            if (vPoint < vVillagePoints[wLeftIndex]) return -1;

            return vPoint >= vVillagePoints[wRightIndex] ? wRightIndex : wLeftIndex;
        }
    }
}
