using System;
using System.Linq;

/*
 <問題>
 各桁が 1 以上 9 以下の整数である 3 桁の整数 N が与えられます。
 N の 100 の位を a、10 の位を b、1 の位を c としたとき、b,c,a をこの順に並べた整数と c,a,b をこの順に並べた整数を
 それぞれ出力してください。

 <制約>
 ・N は各桁が 1 以上 9 以下の整数である 3 桁の整数

 <入力>
 N

 <出力>
 b,c,a をこの順に並べた整数と c,a,b をこの順に並べた整数をこの順で空白区切りで出力せよ。
 */

namespace A_Cyclic {
    internal class Program {
        static void Main(string[] args) {
            var wDigitsNumbers = Console.ReadLine().ToArray();
            var wBToCToA = new string(new char[] { wDigitsNumbers[1], wDigitsNumbers[2], wDigitsNumbers[0] });
            var wCToAToB = new string(new char[] { wDigitsNumbers[2], wDigitsNumbers[0], wDigitsNumbers[1] });
            Console.WriteLine(wBToCToA + " " + wCToAToB);
        }
    }
}
