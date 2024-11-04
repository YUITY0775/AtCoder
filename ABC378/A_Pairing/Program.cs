using System;
using System.Linq;

/*
 <問題>
 4 個のボールがあり、i 個目のボールの色は A(i) です。
 同じ色のボールを 2 つ選び両方捨てるという操作を最大何回行えるか求めてください。

 <制約>
 ・A(1), A(2), A(3), A(4) はそれぞれ 1 以上 4 以下の整数

 <入力>
 A(1) A(2) A(3) A(4)

 <出力>
 操作回数の最大値を整数として出力せよ。
 */

namespace A_Pairing {
    internal class Program {
        static void Main(string[] args) {

            var wBalls = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            var wColorsCount = new int[wBalls.Length];
            for (int i = 1; i <= 4; i++) {
                int wCount = 0;
                for (int j = 0; j < wBalls.Length; j++) {
                    if (wBalls[j] == i) wCount++;
                }
                wColorsCount[i - 1] = wCount;
            }
            int wAnswer = 0;
            for (int i = 0; i < wColorsCount.Length; i++) {
                if (wColorsCount[i] == 2 || wColorsCount[i] == 3) {
                    wAnswer++;
                }
                if (wColorsCount[i] == 4) {
                    wAnswer += 2;
                }
            }
            Console.WriteLine(wAnswer);
        }
    }
}
