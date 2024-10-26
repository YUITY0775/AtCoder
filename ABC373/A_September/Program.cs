using System;

/*
 <問題>
 英小文字からなる 12 個の文字列 S(1), S(2), …, S(12) があります。
 S(i) の長さが i であるような整数 i (1 ≤ i ≤ 12) がいくつあるか求めてください。

 <制約>
 ・S(i) は英小文字からなる長さ 1 以上 100 以下の文字列である。(1 ≤ i ≤ 12)

 <入力>
 S(1)
 S(2)
 …
 S(12)

 <出力>
 S(i) の長さが i であるような整数 i (1 ≤ i ≤ 12) がいくつあるか出力せよ。
 */

namespace A_September {
    internal class Program {
        static void Main(string[] args) {

            var wCount = 0;

            for (int i = 1; i <= 12; i++) {
                if (Console.ReadLine().Length == i) wCount++;
            }
            Console.WriteLine(wCount);
        }
    }
}
