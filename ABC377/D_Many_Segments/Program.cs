using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 長さ N の正整数列 L = (L(1), L(2), …, L(N)), R = (R(1), R(2), …, R(N)) と整数 M が与えられます。
 以下の条件を共に満たす整数の組 (l, r) の個数を求めてください。

 ・1 ≤ l ≤ r ≤ M
 ・全ての 1 ≤ i ≤ N に対し区間 [l, r] は区間 [L(i), R(i)] を完全には含まない。

 <制約>
 ・1 ≤ N, M ≤ 2×10^5
 ・1 ≤ L(i) ≤ R(i) ≤ M
 ・入力は全て整数

 <入力>
 N M
 L(1) R(1)
 L(2) R(2)
 …
 L(N) R(N)

 <出力>
 答えを出力せよ。
 */

namespace D_Many_Segments {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wInputLine = Console.ReadLine().Split(' ');
            (int wSequenceLength, int wEndOfSection) = (int.Parse(wInputLine[0]), int.Parse(wInputLine[1]));

            var wSections = new (int, int)[wSequenceLength];
            for (int i = 0; i < wSequenceLength; i++) {
                var wLine = Console.ReadLine().Split(' ');
                wSections[i] = (int.Parse(wLine[0]) - 1, int.Parse(wLine[1]) - 1);
            }

            // 出力
            Console.WriteLine(CountSections(wEndOfSection, wSections));
        }

        /// <summary>
        /// r を固定し、各 r のときの条件を満たす l の最小値を利用して、条件を満たす区間数を求める
        /// </summary>
        /// <param name="vEndOfSection">区間の右端が取りうる最大値</param>
        /// <param name="vSections">区間の集合</param>
        /// <returns>条件をみたす区間数</returns>
        static long CountSections(int vEndOfSection, IList<(int, int)> vSections) {

            var wMaxLeft = Enumerable.Repeat(0, vEndOfSection).ToArray();
            foreach ((int Left, int Right) wSection in vSections) {
                wMaxLeft[wSection.Right] = Math.Max(wMaxLeft[wSection.Right], wSection.Left + 1);
            }

            for (int r = 1; r < vEndOfSection; r++) {
                wMaxLeft[r] = Math.Max(wMaxLeft[r], wMaxLeft[r - 1]);
            }

            long wNoDuplicationSections = 0;
            for (int r = 0; r < vEndOfSection; r++) {
                wNoDuplicationSections += r - wMaxLeft[r] + 1; 
            }
            return wNoDuplicationSections;
        }
    }
}
