using System;
using System.Linq;

/*
 <問題>
 周期 N をもつ無限数列 A = (A(1), A(2), A(3), …) の先頭 N 項 A(1), A(2), …, A(N) が与えられます。
 この数列の空でない連続する部分列のうち、和が S となるものが存在するか判定してください。
 ただし、無限数列 A が周期 N をもつとは、i > N を満たすすべての整数 i に対して A(i) = A(i - N) が成り立つことをいいます。

 <制約>
 ・1 ≤ N ≤ 2×10^5
 ・1 ≤ A(i) ≤ 10^9
 ・1 ≤ S ≤ 10^8
 ・入力はすべて整数

 <入力>
 N S
 A(1) A(2) … A(N)

 <出力>
 A の連続する部分列 (A(l), A(l + 1), …, A(r)) であって、A(l) + A(l + 1) + … + A(r) = S となるものが存在するならば Yes を、
 そうでないならば No を出力せよ。
 */

namespace D_Repeated_Sequence {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wLineInfo = Console.ReadLine().Split(' ').ToArray();
            (int wPeriod, long wTotal) = (int.Parse(wLineInfo[0]), long.Parse(wLineInfo[1]));
            var wNumbers = Console.ReadLine().Split().Select(x => long.Parse(x)).ToArray();

            // 出力
            Console.WriteLine(ExistsPartialSum(wTotal, wNumbers) ? "Yes" : "No");
        }

        /// <summary>
        /// 与えられた数列を一周期とする無限数列の部分和に、目標値となるものが存在するか否かを返す
        /// </summary>
        /// <param name="vGoal">目標値</param>
        /// <param name="vNumbers">数列</param>
        /// <returns>目標値となる部分和が存在するか否か</returns>
        static bool ExistsPartialSum(long vGoal, long[] vNumbers) {

            // 数列一周期分の累積和を保持
            var wCumulativeSum = new long[vNumbers.Length + 1];
            for (int i = 0; i < vNumbers.Length; i++) {
                wCumulativeSum[i + 1] = wCumulativeSum[i] + vNumbers[i];
            }

            // 一周期分の合計値で割った余りだけ考慮すればよい
            vGoal %= wCumulativeSum[wCumulativeSum.Length - 1];

            // 一周期分内の部分和が条件を満たす場合
            int wRightIndex = 0;
            for (int wLeftIndex = 0; wLeftIndex < wCumulativeSum.Length; wLeftIndex++) {
                while (wRightIndex < wCumulativeSum.Length) {
                    if (vGoal == wCumulativeSum[wRightIndex] - wCumulativeSum[wLeftIndex]) return true;
                    if (vGoal < wCumulativeSum[wRightIndex] - wCumulativeSum[wLeftIndex]) break;
                    wRightIndex++;
                }
            }

            // 周期を跨いだ部分和が条件を満たす場合
            wRightIndex = 0;
            for (int wLeftIndex = 0; wLeftIndex < wCumulativeSum.Length; wLeftIndex++) {
                while (wRightIndex < wCumulativeSum.Length) {
                    long wRestSum = wCumulativeSum[wCumulativeSum.Length - 1] - (wCumulativeSum[wRightIndex] - wCumulativeSum[wLeftIndex]);
                    if (vGoal == wRestSum) return true;
                    if (vGoal > wRestSum) break;
                    wRightIndex++;
                }
            }

            return false;
        }
    }
}
