using System;
using System.Text.RegularExpressions;

/*
 <問題>
 英小文字および . からなる文字列 S が与えられます。S から . を全て削除した文字列を求めてください。

 <制約>
 ・S は英小文字および . からなる長さ 1 以上 100 以下の文字列

 <入力>
 S

 <出力> 
 S から . をすべて削除した文字列を出力せよ。
 */

namespace A_Delete_Period {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine(Regex.Replace(Console.ReadLine(), "\\.", ""));
        }
    }
}
