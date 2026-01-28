using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc442/tasks/abc442_c
 */

namespace C_PeerReview {
    internal class Program {
        static void Main(string[] args) {

            const int ASSESSOR_COUNT = 3;

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            (int researchersCount, int stakeholdersCount) = (inputs[0], inputs[1]);

            // 各研究者の利害関係者数を配列で管理する
            var stakeholderCounter = new int[researchersCount + 1];
            for (int i = 0; i < stakeholdersCount; i++) {
                var stakeholderInfo = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                stakeholderCounter[stakeholderInfo[0]]++;
                stakeholderCounter[stakeholderInfo[1]]++;
            }

            //出力処理
            var answers = new List<long>();
            for (int i = 1; i <= researchersCount; i++) {
                answers.Add(CalcCombination(researchersCount - 1 - stakeholderCounter[i], ASSESSOR_COUNT));
            }
            Console.WriteLine(string.Join(" ", answers));
        }

        /// <summary>
        /// nCr を計算する（組み合わせ）
        /// </summary>
        static long CalcCombination(int n, int r) {
            // 対称性を利用して計算量削減
            r = Math.Min(r, n - r);
            if (r < 0) return 0;

            long result = 1;
            for (int i = 1; i <= r; i++) {
                result = result * (n - i + 1) / i;
            }
            return result;
        }
    }
}
