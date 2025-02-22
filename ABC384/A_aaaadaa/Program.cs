using System;
using System.Linq;
using System.Text;

/*
 <問題>
 長さ N の英小文字からなる文字列 S と英小文字 c(1), c(2) が与えられます。 
 S の文字のうち c(1) であるもの 以外 を全て c(2) に置き換えた文字列を求めてください。

 <制約>
 ・1 ≤ N ≤ 100
 ・N は整数
 ・c(1), c(2) は英小文字
 ・S は英小文字からなる長さ N の文字列

 <入力>
 N c(1) c(2)
 S

 <出力>
 答えを出力せよ。
 */

namespace A_aaaadaa {
    internal class Program {
        static void Main(string[] args) {
            var wLineInfo = Console.ReadLine().Split(' ').ToArray();
            var wTargetText = Console.ReadLine();
            char wRemaindChar = wLineInfo[1][0];
            char wReplacedChar = wLineInfo[2][0];

            var wAnswerBuilder = new StringBuilder();
            for (int i = 0; i < wTargetText.Length; i++) {
                wAnswerBuilder.Append(wTargetText[i] == wRemaindChar ? wRemaindChar : wReplacedChar);
            }
            Console.WriteLine(wAnswerBuilder);
        }
    }
}
