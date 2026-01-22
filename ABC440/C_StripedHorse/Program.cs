using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc440/tasks/abc440_c
 */

namespace C_StripedHorse {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int testCasesCount = int.Parse(Console.ReadLine());

            // 各テストケースの解を求める
            var answers = new List<long>();
            for (int i = 0; i < testCasesCount; i++) {

                // テストケースの読み取り
                var caseInfo = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var costs = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();

                // テストケースの解を求める
                var testCase = new TestCase(costs, caseInfo[1] * 2);
                answers.Add(testCase.Solve());
            }

            // 出力処理
            Console.WriteLine(string.Join(Environment.NewLine, answers));
        }
    }

    /// <summary>
    /// テストケース
    /// </summary>
    internal class TestCase {

        /// <summary>
        /// コスト配列
        /// </summary>
        private readonly long[] Costs;

        /// <summary>
        /// 周期長
        /// </summary>
        private readonly int CycleLength;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCase(long[] costs, int cycleLength) {
            this.Costs = costs;
            this.CycleLength = cycleLength;
        }

        /// <summary>
        /// テストケースの解を求める
        /// </summary>
        public long Solve() {

            // コスト配列長 <= 半周期のときはすべて選択しないときのコスト 0 が最小
            int halfCycle = this.CycleLength / 2;
            if (this.Costs.Length <= halfCycle) return 0;

            // 円環配列を考え、各コスト配列のコストを対応する周期位置に加算する
            var costsRing = new long[this.CycleLength];
            for (int i = 0; i < this.Costs.Length; i++) {
                int position = i % this.CycleLength;
                costsRing[position] += this.Costs[i];
            }

            // 円環配列を直線化し、左端からスライディングウィンドウ法で最小値を求める
            var costsLine = new long[2 * this.CycleLength];
            for (int i = 0; i < this.CycleLength; i++) {
                costsLine[i] = costsRing[i];
                costsLine[i + this.CycleLength] = costsRing[i];
            }

            long answer = long.MaxValue;
            long current= costsLine.Take(halfCycle).Sum(x => x);
            for (int i = halfCycle; i < 3 * halfCycle; i++) {
                current = current + costsLine[i] - costsLine[i - halfCycle];
                answer = Math.Min(answer, current);
            }
            return answer;
        }
    }
}
