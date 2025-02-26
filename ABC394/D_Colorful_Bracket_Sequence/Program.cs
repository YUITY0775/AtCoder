using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 6 種類の文字、(, ), [, ], <, > からなる文字列 S が与えられます。
 ここで、文字列 T は、以下の条件を満たすときカラフル括弧列と呼ばれます。

 以下の操作を何回か（0 回でも良い）繰り返すことで、T を空文字列にできる。
    ・T の（連続する）部分文字列であって、(), [], <> のいずれかであるようなものが存在するとき、そのうちの 1 つを選んで削除する。
    ・削除された部分文字列が T の先頭または末尾であるとき、残りの文字列を新たに T とする。
    ・そうでないとき、削除された前後の文字列を 1 つに連結し、新たに T とする。

 S がカラフル括弧列であるか判定してください。

 <制約>
 ・S は長さ 1 以上 2×10^5 以下の文字列
 ・S は (, ), [, ], <, > のみからなる。

 <入力>
 S

 <出力>
 S がカラフル括弧列ならば Yes を、そうでないならば No を出力せよ。
 */

namespace D_Colorful_Bracket_Sequence {
    internal class Program {
        static void Main(string[] args) {
            string wBracketsLine = Console.ReadLine();
            Console.WriteLine(IsColorfulString(wBracketsLine) ? "Yes" : "No");
        }

        /// <summary>
        /// 括弧のパターンを保持する連想配列
        /// </summary>
        static Dictionary<char, char> FBracketsBeginToEnd = new Dictionary<char, char>() {
            { '(', ')' },
            { '[', ']' },
            { '<', '>' },
        };

        /// <summary>
        /// 与えられた文字列がカラフル文字列か否かを返す
        /// </summary>
        static bool IsColorfulString(string vBracketsLine) {
            var wBracketsStack = new Stack<char>();
            for (int i = 0; i < vBracketsLine.Length; i++) {
                if (IsBegin(vBracketsLine[i])) {
                    wBracketsStack.Push(vBracketsLine[i]);
                    continue;
                }
                // for文の途中でスタックが空になる場合と取り出した文字が見ている文字とペアじゃない場合はカラフル文字列になり得ない
                if (wBracketsStack.Count <= 0 || FBracketsBeginToEnd[wBracketsStack.Peek()] != vBracketsLine[i]) return false;

                wBracketsStack.Pop();
            }
            return wBracketsStack.Count == 0;
        }

        /// <summary>
        /// 与えられた文字が括弧の始まりに該当するか否かを返す
        /// </summary>
        static bool IsBegin(char vBracket) => FBracketsBeginToEnd.Keys.Contains(vBracket);
    }
}
