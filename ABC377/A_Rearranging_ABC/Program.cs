using System;
using System.Linq;

/*
 <問題>
 長さ 3 の英大文字からなる文字列 S が与えられます。
 S の各文字を並び替えることで S を文字列 ABC と一致させることができるか判定してください。

 <制約>
 ・S は英大文字からなる長さ 3 の文字列

 <入力>
 S

 <出力>
 S の各文字を並び替えることで文字列 ABC と一致させることができるなら Yes を、そうでないなら No を出力せよ。
 */

namespace A_Rearranging_ABC {
    internal class Program {
        static void Main(string[] args) {

            var wInputText = Console.ReadLine();

            Func<string, char, bool> wHasAlphabet = (vText, vAlphabet) => vText.Any(x => x == vAlphabet);

            if (wHasAlphabet(wInputText, 'A') && wHasAlphabet(wInputText, 'B') && wHasAlphabet(wInputText, 'C')) {
                Console.WriteLine("Yes");
                return;
            }
            Console.WriteLine("No");
        }
    }
}
