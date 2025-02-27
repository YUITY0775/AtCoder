using System;
using System.Linq;

/*
 <問題>
 1 から N の番号がついた N 人の人がいます。
 人 i は数 Q(i) が書かれたゼッケンを着けており、人 P(i) を見つめています。
 i が書かれたゼッケンを着けている人が見つめている人の着けているゼッケンにかかれている数を、
 i = 1, 2, …, N のそれぞれについて求めてください。

 <制約>
 ・2 ≤ N ≤ 3×10^5
 ・1 ≤ P(i) ≤ N
 ・P(i) は相異なる
 ・1 ≤ Q(i) ≤ N
 ・Q(i) は相異なる
 ・入力は全て整数である

 <制約>
 N
 P(1) P(2) … P(N)
 Q(1) Q(2) … Q(N)

 <出力>
 i が書かれたゼッケンを着けている人が見つめている人の着けているゼッケンにかかれている数を S(i) とする。
 S(1), S(2), …, S(N) をこの順に空白区切りで出力せよ。
 */

namespace C_Bib {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wTotalPersons = int.Parse(Console.ReadLine());
            var wSightNumbers = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var wBibNumbers = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            // S(Q(i)) = Q(P(i) であることを利用する
            var wAnswer = new int[wTotalPersons];
            for (int i = 0; i < wTotalPersons; i++) {
                wAnswer[wBibNumbers[i] - 1] = wBibNumbers[wSightNumbers[i] - 1];
            }

            // 出力
            Console.WriteLine(string.Join(" ", wAnswer));
        }
    }
}
