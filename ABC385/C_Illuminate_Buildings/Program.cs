using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 N 棟のビルが等間隔に一列に並んでいます。手前から i 番目のビルの高さは H(i) です。
 あなたは次の条件をともに満たすようにいくつかのビルを選んで電飾で飾ろうとしています。

 ・選んだビルたちは高さが等しい
 ・選んだビルたちは等間隔に並んでいる

 最大でいくつのビルを選ぶことができますか？　なお、ちょうど 1 つのビルを選んだときは条件を満たすとみなします。

 <制約>
 ・1 ≤ N ≤ 3000
 ・1 ≤ H(i) ≤ 3000
 ・入力は全て整数である

 <入力>
 N
 H(1) … H(N)

 <出力>
 答えを出力せよ。
 */

namespace C_Illuminate_Buildings {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wBuildingsCount = int.Parse(Console.ReadLine());
            var wHeightsInfo = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var wHeightToCoordinates = new Dictionary<int, List<int>>();
            for (int i = 0; i < wBuildingsCount; i++) {
                if (wHeightToCoordinates.TryGetValue(wHeightsInfo[i], out List<int> wCoordinates)) {
                    wCoordinates.Add(i);
                } else {
                    wHeightToCoordinates.Add(wHeightsInfo[i], new List<int>() { i });
                }
            }

            // 高さごとに等間隔で存在するビルの棟数を求め、最大値を保持する
            int wMaxBuildingsCount = 0;
            foreach (var wCoordinates in wHeightToCoordinates.Values) {
                int wCurrentBuildingsCount = CalcMaxTermsCount(wCoordinates);
                wMaxBuildingsCount = Math.Max(wCurrentBuildingsCount, wMaxBuildingsCount);
            }

            // 出力
            Console.WriteLine(wMaxBuildingsCount);
        }

        /// <summary>
        /// 数列から任意の等差数列を抽出するとき、項数が取り得る最大値を求める
        /// </summary>
        /// <param name="vNumbers">数列</param>
        /// <returns>任意の等差数列の項数が取り得る最大値</returns>
        static int CalcMaxTermsCount(IList<int> vNumbers) {
            var wDynamicProgramming = new Dictionary<int, int>[vNumbers.Count];
            for (int i = 0; i < vNumbers.Count; i++) {
                wDynamicProgramming[i] = new Dictionary<int, int>();
            }

            // 最低 1 棟は選ぶため、初期値を 1 としておく
            int wMaxCount = 1;
            for (int i = 0; i < vNumbers.Count; i++) {
                for (int j = 0; j < i; j++) {
                    int wDiff = vNumbers[i] - vNumbers[j];
                    if (!wDynamicProgramming[j].TryGetValue(wDiff, out int wCount)) wDynamicProgramming[j][wDiff] = 1;
                    wDynamicProgramming[i][wDiff] = wDynamicProgramming[j][wDiff] + 1;
                    wMaxCount = Math.Max(wMaxCount, wDynamicProgramming[i][wDiff]);
                }
            }
            return wMaxCount;
        }
    }
}
