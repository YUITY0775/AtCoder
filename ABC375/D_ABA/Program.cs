using System;

/*
 <問題>
 英大文字からなる文字列 S が与えられます。
 整数の組 (i, j, k) であって、以下の条件をともに満たすものの個数を求めてください。

 ・1 ≤ i < j < k ≤ ∣S∣
 ・S(i), S(j), S(k) をこの順に結合して得られる長さ 3 の文字列が回文となる

 ただし、∣S∣ は文字列 S の長さ、S(x) は S の x 番目の文字を指します。
​
 <制約>
 ・S は長さ 1 以上 2×10^5 以下の英大文字からなる文字列

 <入力>
 S

 <出力>
 答えを出力せよ。
 */

namespace D_ABA {
    internal class Program {
        static void Main(string[] args) {
            var wInputText = Console.ReadLine();

            long[] wAlphabetCount = new long[26];
            long[] wSumOfIndex = new long[26];

            long  wAnswer = 0;
            for (int i = 0; i < wInputText.Length; i++) {
                var wIndex = wInputText[i] - 'A';

                wAnswer += (i - 1) * wAlphabetCount[wIndex] - wSumOfIndex[wIndex];

                wAlphabetCount[wIndex]++;
                wSumOfIndex[wIndex] += i;
            }
            Console.WriteLine(wAnswer);
        }
    }
}
