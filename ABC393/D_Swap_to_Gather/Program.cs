using System;

/*
 <問題>
 0 および 1 からなる長さ N の文字列 S が与えられます。 ここで、S には 1 が 1 つ以上含まれることが保証されます。
 あなたは、以下の操作を繰り返し（0 回でも良い）行うことができます。

 1 ≤ i ≤ N − 1 を満たす整数 i を選び、S の i 文字目と i + 1 文字目を入れ替える。
 すべての 1 がひとかたまりになった状態にするために必要な操作回数の最小値を求めてください。

 なお、すべての 1 がひとかたまりになっているとは、ある整数 l, r (1 ≤ l ≤ r ≤ N) が存在し、S の i 文字目は l ≤ i ≤ r ならば 1、
 そうでないならば 0 であることをいいます。

 <制約>
 ・2 ≤ N ≤ 5×10^5
 ・N は整数
 ・S は 0 および 1 からなる長さ N の文字列
 ・S には 1 が 1 つ以上含まれる

 <入力>
 N
 S

 <出力>
 答えを出力せよ。
 */

namespace D_Swap_to_Gather {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            (_, string wReadLine) = (Console.ReadLine(), Console.ReadLine());

            // 1 の個数の累積和を配列に格納する
            int wOneCount = 0;
            var wOneCumulativeSum = new int[wReadLine.Length];
            for (int i = 0; i < wReadLine.Length; i++) {
                if (wReadLine[i] == '1') wOneCount++;
                wOneCumulativeSum[i] = wOneCount;
            }

            // 各 0 について移動回数を累積する
            long wMovingCount = 0;
            for (int i = 0; i < wReadLine.Length; i++) {
                if (wReadLine[i] == '0') {
                    wMovingCount += Math.Min(wOneCumulativeSum[i], wOneCumulativeSum[wOneCumulativeSum.Length - 1] - wOneCumulativeSum[i]);
                }
            }

            // 出力
            Console.WriteLine(wMovingCount);
        }
    }
}
