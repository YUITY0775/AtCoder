using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 長さ M の整数列 A =(A(1), A(2), …, A(M)) が与えられます。A の各要素は 1 以上 N 以下で、全ての要素は相異なります。
 A の要素として含まれない 1 以上 N 以下の整数を、昇順に全て列挙してください。

 <制約>
 ・入力は全て整数
 ・1 ≤ M ≤ N ≤ 1000
 ・1 ≤ A(i) ≤ N
 ・A の要素は相異なる

 <入力>
 N M
 A(1) A(2) … A(M)

 <出力>
 A の要素として含まれない 1 以上 N 以下の整数を昇順に全て挙げた列が (X(1), X(2), …, X(C)) であるとき、以下の形式で出力せよ。
 C
 X(1) X(2) … X(C)
 */

namespace B_Who_is_Missing {
    internal class Program {
        static void Main(string[] args) {
            var wReadLine = Console.ReadLine().Split(' ');
            (int wMaxNumber, int wLength) = (int.Parse(wReadLine[0]), int.Parse(wReadLine[1]));
            var wUniqueNumbers = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToHashSet();

            var wNoIncludedNumbers = new List<int>();
            for (int i = 1; i <= wMaxNumber; i++) {
                if (wUniqueNumbers.Contains(i)) continue;
                wNoIncludedNumbers.Add(i);
            }
            Console.WriteLine(wNoIncludedNumbers.Count + Environment.NewLine + string.Join(" ", wNoIncludedNumbers));
        }
    }
}
