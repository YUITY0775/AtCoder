using System;
using System.Collections.Generic;

/*
 <問題>
 1 から N まで番号がつけられた N 個のマスが一列に並んでいます。最初、 M 個のマスに石が入っており、マス X(i) には
 A(i) 個 (1 ≤ i ≤ M) の石が入っています。
 あなたは以下の操作を好きな回数 (0 回でもよい) 行うことができます。

 マス i (1 ≤ i ≤ N − 1) に石があるとき、マス i から石を 1 つマス i + 1 に移動させる。
 N 個のマスすべてに石がちょうど 1 個ずつ入っている状態にするために必要な操作回数の最小値を求めてください。
 ただし、不可能な場合は −1 を出力してください。

 <制約>
 ・2 ≤ N ≤ 2×10^9
 ・1 ≤ M ≤ 2×10^5
 ・M ≤ N
 ・1 ≤ X(i) ≤ N (1 ≤ i ≤ M)
 ・X(i) != X(j) (1 ≤ i < j ≤ M)
 ・1 ≤ A(i) ≤ 2×10^9 (1 ≤ i ≤ M)
 ・入力は全て整数

 <入力>
 N M
 X(1) X(2) … X(M)
 A(1) A(2) … A(M)

 <出力>
 答えを出力せよ。
 */

namespace C_Sowing_Stones {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wSquareCount = int.Parse(Console.ReadLine().Split(' ')[0]);

            var wSquareIndexes = Console.ReadLine().Split(' ');
            var wStonesPerSquare = Console.ReadLine().Split(' ');

            // 長さ N の配列を作成するとメモリ消費が大きいため、石があるマスと石の個数を連想配列で保持する
            var wSquareIndexToStones = new SortedDictionary<int, int>();
            for (int i = 0; i < wSquareIndexes.Length; i++) {
                wSquareIndexToStones.Add(int.Parse(wSquareIndexes[i]), int.Parse(wStonesPerSquare[i]));
            }

            // 出力
            Console.WriteLine(CountMovingStones(wSquareIndexToStones, wSquareCount));
        }

        static long CountMovingStones(SortedDictionary<int, int> vSquareIndexToStones, int vSquareCount) {
            if (!CanMakeEven(vSquareIndexToStones, vSquareCount)) return -1;

            // 計算方法について説明しておく
            long w = 0;
            foreach (var wIndexStonesPair in vSquareIndexToStones) {
                w += wIndexStonesPair.Key * wIndexStonesPair.Value;
            }
            return ((vSquareCount + 1) * vSquareCount) / 2 - w;
        }

        /// <summary>
        /// すべてのマスに石を1つずつ配置できるか否かを判定する
        /// </summary>
        /// <param name="vSquareIndexToStones">石の個数</param>
        /// <param name="vSquareCount">マスの数</param>
        /// <returns>すべてのマスに石を1つずつ配置できる場合にtrueを返す</returns>
        static bool CanMakeEven(SortedDictionary<int, int> vSquareIndexToStones, int vSquareCount) {
            long wCumulativeStoneCount = 0; // 石の個数の累積和
            int wPrevIndex = 0;
            foreach (var wIndexStonesPair in vSquareIndexToStones) {
                wCumulativeStoneCount += wIndexStonesPair.Value;
                if (wCumulativeStoneCount < wIndexStonesPair.Key) return false;
            }
            return wCumulativeStoneCount == vSquareCount;
        }
    }
}
