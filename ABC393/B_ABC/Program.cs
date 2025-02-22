using System;
using System.Collections.Generic;

/*
 <問題>
 文字列 S が与えられます。S の中に A, B, C がこの順に等間隔に並んでいる場所が何箇所あるか求めてください。
 厳密には、3 つの整数の組 (i, j, k) であって、以下の条件をすべて満たすものの個数を求めてください。
 ここで、∣S∣ で S の長さを、S(x) で S の x 文字目を表すものとします。

 ・1 ≤ i < j < k ≤ ∣S∣
 ・j − i = k − j
 ・S(i) = A
 ・S(j) = B
 ・S(k) = C

 <制約>
 ・S は英大文字からなる長さ 3 以上 100 以下の文字列

 <入力>
 S

 <出力>
 答えを出力せよ。
 */

namespace B_ABC {
    internal class Program {
        static void Main(string[] args) {

            string wReadLine = Console.ReadLine();

            int wA_B_CCount = 0;
            var wAIndexes = new List<int>();
            for (int i = 0; i < wReadLine.Length; i++) {
                if (wReadLine[i] == 'B') wA_B_CCount += CountA_B_C(wReadLine, i, wAIndexes);
                if (wReadLine[i] == 'A') {
                    wAIndexes.Add(i);
                }
            }
            Console.WriteLine(wA_B_CCount);
        }

        /// <summary>
        /// 与えられた B を含む、等間隔で並ぶ ABC が文字列中にいくつ存在するかを返す。
        /// </summary>
        /// <param name="vText">対象文字列</param>
        /// <param name="vBIndex">B のインデックス</param>
        /// <param name="vAIndexes"> 与えられた B より左にある A のインデックス </param>
        /// <returns>等間隔で並ぶ ABC の数</returns>
        static int CountA_B_C(string vText, int vBIndex, IEnumerable<int> vAIndexes) {
            int wCount = 0;
            foreach (int vAIndex in vAIndexes) {
                int wIndex = vBIndex + (vBIndex - vAIndex);
                if (wIndex < 0 || vText.Length - 1 < wIndex) continue;
                if (vText[wIndex] == 'C') wCount++;
            }
            return wCount;
        }
    }
}
