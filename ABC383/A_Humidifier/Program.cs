using System;
using System.Collections.Generic;

/*
 <問題>
 AtCoder社のオフィスには加湿器が 1 つあります。現在は時刻 0 であり、加湿器には水が入っていません。
 あなたはこの加湿器にこれから 
 N 回水を追加します。i 回目 (1 ≤ i ≤ N)の水の追加は時刻 T(i) に行い、水を V(i) リットル追加します。
 なお、T(i) < T(i + 1) (1 ≤ i ≤ N − 1) を満たすことが保証されます。
 ただし、加湿器には穴が空いていて、加湿器に水が入っている間は 
 1 単位時間につき水が 1 リットル減り続けます。
 時刻 T(N) に水を追加し終えたとき、加湿器に残っている水の量を求めてください。

 <制約>
 ・1 ≤ N ≤ 100
 ・1 ≤ T(i) ≤ 100 (1 ≤ i ≤ N)
 ・1 ≤ V(i) ≤ 100 (1 ≤ i ≤ N)
 ・T(i) < T(i + 1) (1 ≤ i ≤ N − 1)
 ・入力は全て整数

 <入力>
 N
 T(1) V(1)
 T(2) V(2)
 …
 T(N) V(N)

 <出力>
 答えを出力せよ。
 */

namespace A_Humidifier {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wTimeAmountPairCount = int.Parse(Console.ReadLine());
            var wTimeAmountPairs = new List<(int Time, int Amount)>();
            for (int i = 0; i < wTimeAmountPairCount; i++) {
                var wLineInfo = Console.ReadLine().Split(' ');
                wTimeAmountPairs.Add((int.Parse(wLineInfo[0]), int.Parse(wLineInfo[1])));
            }

            // 時刻 T(i) の水の量 = V(i) + 時刻 T(i - 1) の水の量 - (T(i) - T(i - 1))
            int wCurrentAmount = wTimeAmountPairs[0].Amount;
            for (int i = 1; i < wTimeAmountPairs.Count; i++) {
                int wTimeDiff = wTimeAmountPairs[i].Time - wTimeAmountPairs[i - 1].Time;
                wCurrentAmount = wTimeAmountPairs[i].Amount + Math.Max(wCurrentAmount - wTimeDiff, 0);
            }

            // 出力
            Console.WriteLine(wCurrentAmount);
        }
    }
}
