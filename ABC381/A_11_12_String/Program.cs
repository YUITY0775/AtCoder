using System;

/*
 <問題>
 ※この問題における 11/22 文字列の定義は C 問題と同じです。

 文字列 T が以下の条件を全て満たすとき、T を 11/22 文字列 と呼びます。

 ・∣T∣ は奇数である。ここで、∣T∣ は T の長さを表す。
 ・1 文字目から (∣T∣ + 1)/2 - 1 文字目までが 1 である。
 ・(∣T∣ + 1)/2 文字目が / である。
 ・(∣T∣ + 1)/2 + 1 文字目から ∣T∣ 文字目までが 2 である。

 例えば 11/22, 111/222, / は 11/22 文字列ですが、1122, 1/22, 11/2222, 22/11, //2/2/211 はそうではありません。

 1, 2, / からなる長さ N の文字列 S が与えられます。S が 11/22 文字列であるか判定してください。

 <制約>
 ・1 ≤ N ≤ 100
 ・S は 1, 2, / からなる長さ N の文字列

 <入力>
 N
 S

 <出力>
 S が 11/22 文字列であれば Yes を、そうでなければ No を出力せよ。
 */

namespace A_11_12_String {
    internal class Program {
        static void Main(string[] args) {
            int _ = int.Parse(Console.ReadLine());
            string wText = Console.ReadLine();
            Console.WriteLine(Is11_22String(wText) ? "Yes" : "No");
        }

        /// <summary>
        /// 対象文字列が 11/22 文字列であるか否かを返す
        /// </summary>
        /// <param name="vText">対象文字列</param>
        /// <returns>対象文字列が 11/22 文字列か否か</returns>
        static bool Is11_22String(string vText) {
            if (vText.Length % 2 == 0) return false;

            int wMidIndex = (vText.Length - 1) / 2;
            if (vText[wMidIndex] != '/') return false;
            for (int i = 0; i < wMidIndex; i++) {
                if (vText[i] != '1') return false;
            }
            for (int i = vText.Length - 1; i > wMidIndex; i--) {
                if (vText[i] != '2') return false;
            }
            return true;
        }
    }
}
