using System;
using System.Linq;

/*
 <問題>
 不思議なボタンがあります。 このボタンを押すと飴を 1 つもらえますが、前回飴をもらってからの経過時間が C 秒未満である場合はもらえません。
 高橋君はこのボタンを N 回押してみることにしました。i 回目にボタンを押すのは今から T(i) 秒後です。
 高橋君は何個の飴をもらうことができますか？

 <制約>
 ・1 ≤ N ≤ 100
 ・1 ≤ C ≤ 1000
 ・0 ≤ T(1) < T(2) < … < T(N) ≤ 1000
 ・入力は全て整数

 <入力>
 N C
 T(1) T(2) … T(N)

 <出力>
 高橋君がもらうことのできる飴の個数を出力せよ。
 */

namespace A_Candy_Button {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wInputLine = Console.ReadLine().Split(' ');
            var wInputCount = int.Parse(wInputLine[0]);
            var wInterval = int.Parse(wInputLine[1]);
            var wPressedTimes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int wCandyCount = 1;
            int wPreviousTime = wPressedTimes[0];
            for (int i = 1; i < wPressedTimes.Count(); i++) {
                if (wPressedTimes[i] - wPreviousTime >= wInterval) {
                    wCandyCount++;
                    wPreviousTime = wPressedTimes[i];
                }
            }
            Console.WriteLine(wCandyCount);
        }
    }
}
