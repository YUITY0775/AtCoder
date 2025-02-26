using System;

/*
 <問題>
 数字からなる文字列 S が与えられます。
 S から 2 以外の文字を削除し、残った文字を順序を保って結合した文字列を求めてください。

 <制約>
 ・S は数字からなる長さ 1 以上 100 以下の文字列
 ・S は 2 を 1 つ以上含む

 <入力>
 S

 <出力>
 答えを出力せよ。
 */

namespace A_22222 {
    internal class Program {
        static void Main(string[] args) {
            string wReadLine = Console.ReadLine();
            int wTwoCounter = 0;
            for (int i = 0; i < wReadLine.Length; i++) {
                if (wReadLine[i] == '2') wTwoCounter++;
            }
            Console.WriteLine(new string('2', wTwoCounter));
        }
    }
}
