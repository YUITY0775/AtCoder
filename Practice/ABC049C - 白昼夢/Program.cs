using System;
using System.Linq;

//<問題文>
//英小文字からなる文字列 S が与えられます。 T が空文字列である状態から始め、
//以下の操作を好きな回数繰り返すことで S=T とすることができるか判定してください。

// T の末尾に dream dreamer erase eraser のいずれかを追加する。

//<制約>
//1≦∣S∣≦10^5
// S は英小文字からなる。

//<入力>
// S

//<出力>
// S=T とすることができる場合 YES を、そうでない場合 NO を出力せよ。

namespace ABC049C___白昼夢 {
    internal class Program {
        static void Main(string[] args) {
            string wTarget = Console.ReadLine();

            Console.WriteLine(CheckText(wTarget)? "YES": "NO");
        }

        private static bool CheckText(string vText) {

            var wWords = new[] { "dream", "dreamer", "erase", "eraser" };
            var wReversedWords = wWords.Select(x => string.Join("",x.Reverse()));

            var wReversedText = new string (vText.Reverse().ToArray());

            while (wReversedText.Length > 0) {
                bool wIsExist = false;
                foreach (var wWord in wReversedWords) {
                    if (wReversedText.Length < wWord.Length) continue;
                    if (wWord == wReversedText.Substring(0, wWord.Length)) {
                        wReversedText = wReversedText.Substring(wWord.Length);
                        wIsExist = true;
                        break;
                    }
                }
                if (!wIsExist) return false;
            }
            return true;
        }
    }
}