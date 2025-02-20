using System;
using System.Collections.Generic;

/*
 <問題>
 文字列 T が以下の 3 つの条件をすべてみたすとき、かつそのときに限り、T を 1122 文字列 と呼びます。

 ・∣T∣ は偶数である。ここで、∣T∣ は T の長さを表す。
 ・1 ≤ i ≤ ∣T∣/2 をみたす整数 i について、T の (2i − 1) 文字目と 2i 文字目は等しい。
 ・各文字は T にちょうど 0 個または 2 個現れる。すなわち、T に含まれる文字は T にちょうど 2 回ずつ登場する。

 英小文字のみからなる文字列 S が与えられるので、S が 1122 文字列であるならば Yes を、そうでないならば No を出力してください。

 <制約>
 S は英小文字のみからなる長さ 1 以上 100 以下の文字列

 <入力>
 S

 <出力>
 S が 1122 文字列ならば Yes を、そうでないならば No を出力せよ。
 */

namespace B_1122_String {
    internal class Program {
        static void Main(string[] args) {
            string wText = Console.ReadLine();
            Console.WriteLine(wText);
        }

        static bool Is1122String(string vText) {
            if (vText.Length % 2 != 0) return false;

            var wUniqueChars = new HashSet<char>();
            for (int i = 0; i < vText.Length / 2 - 1; i++) {
                if (!wUniqueChars.Add(vText[2 * i - 1]) return false;

            }
        }
    }
}
