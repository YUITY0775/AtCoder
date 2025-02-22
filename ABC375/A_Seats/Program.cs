using System;

/*
 <問題>
 N 個の座席が並んでおり、座席には 1,2,…,N の番号が付けられています。
 座席の状態は "#", "." からなる長さ N の文字列 S によって与えられます。
 S の i 文字目が # のとき座席 i には人が座っていることを表し、S の i 文字目が . のとき座席 i には人が座っていないことを表します。
 1 以上 N − 2 以下の整数 i であって、以下の条件を満たすものの個数を求めてください。

 ・座席 i, i + 2 には人が座っており、座席 i + 1 には人が座っていない

 <制約>
 ・N は 1 以上 2 × 10^5 以下の整数
 ・S は "#", "." からなる長さ N の文字列

 <入力>
 N
 S

 <出力>
 答えを出力せよ。
 */

namespace A_Seats {
    internal class Program {
        static void Main(string[] args) {
            var wSeatsCount = int.Parse(Console.ReadLine());
            var InputText = Console.ReadLine();
            string wPattern = "#.#";
            int wCount = 0;

            for (int i = 0; i < wSeatsCount - 2; i++) {
                if (InputText.Substring(i, 3) == wPattern) wCount++;
            }
            Console.WriteLine(wCount);
        }
    }
}
