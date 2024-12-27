using System;
using System.Linq;

/*
 <問題>
 AtCoder社のオフィスは H 行 W 列のマス目で表されます。上から i 行目、左から j 列目のマスを (i, j) と表します。
 各マスの状態は文字 S(i, j) で表され、 S(i, j) が # のときそのマスは机、. のときそのマスは床です。
 床のマスは 2 つ以上存在することが保証されます。

 あなたは床のマスから異なる 2 マスを選び、加湿器を設置します。

 加湿器が設置されたとき、あるマス (i, j) は加湿器があるマス (i′, j′) からのマンハッタン距離が D 以下であるとき、
 またその時に限り加湿されます。

 なお、マス (i, j) からマス (i′, j′) までのマンハッタン距離は ∣i − i′∣ + ∣j − j′∣ で表されます。
 ここで、加湿器が置かれた床のマスは必ず加湿されていることに注意してください。

 加湿される 床のマス の個数として考えられる最大値を求めてください。

 <制約>
 ・1 ≤ H ≤ 10
 ・1 ≤ W ≤ 10
 ・2 ≤ H×W
 ・0 ≤ D ≤ H + W − 2
 ・H, W, D は整数
 ・S(i, j) は # または . である (1 ≤ i ≤ H, 1 ≤ j ≤ W)
 ・床のマスは 2 つ以上存在する

 <入力>
 H W D
 S(1, 1) S(1, 2) … S(1, W)
 S(2, 1) S(2, 2) … S(2, W)
 …
 S(H, 1) S(H, 2) … S(H, W)

 <出力>
 答えを出力せよ。
 */

namespace B_Humidifier {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wLineInfo = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            (int wRowCount, int wColumnCount, int wDistance) = (wLineInfo[0], wLineInfo[1], wLineInfo[2]);

            var wOfficeMap = new bool[wRowCount, wColumnCount];
            for (int i = 0; i < wRowCount; i++) {
                string wRowInfo = Console.ReadLine();
                for (int j = 0; j < wColumnCount; j++) {
                    wOfficeMap[i, j] = wRowInfo[j] == '.';
                }
            }

            // 全探索ですべての床の組み合わせを確かめる
            int wMaxHumidifiedFloorCount = 0;
            for (int i = 0; i < wRowCount; i++) {
                for (int j = 0; j < wColumnCount; j++) {
                    // 床じゃないときはスキップ
                    if (!wOfficeMap[i, j]) continue;
                    for (int s = 0; s < wRowCount; s++) {
                        for (int t = 0; t < wColumnCount; t++) {
                            // 探索中の区画と同じであるときと、床じゃないときはスキップ
                            if (!wOfficeMap[s, t] || (i == s && j == t)) continue;
                            int wHumidifiedFloorCount = 0;
                            for (int p = 0; p < wRowCount; p++) {
                                for (int q = 0; q < wColumnCount; q++) {
                                    if (wOfficeMap[p, q] && (Math.Abs(p - i) + Math.Abs(q - j) <= wDistance || Math.Abs(p - s) + Math.Abs(q - t) <= wDistance)) {
                                        wHumidifiedFloorCount++;
                                    }
                                }
                            }
                            wMaxHumidifiedFloorCount = Math.Max(wHumidifiedFloorCount, wMaxHumidifiedFloorCount);
                        }
                    }
                }
            }

            // 出力
            Console.WriteLine(wMaxHumidifiedFloorCount);
        }
    }
}
