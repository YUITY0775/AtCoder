using System;
using System.Linq;

/*
 <問題>
 整数列 A = (A(1), A(2), A(3)) が与えられます。A の要素を自由に並べ替えた列を B = (B(1), B(2), B(3)) とします。
 このとき、 B(1) × B(2) = B(3) を満たすようにできるか判定してください。

<制約>
 ・入力は全て整数
 ・1 ≤ A(1), A(2), A(3) ≤ 100

 <入力>
 A(1), A(2), A(3)

 <出力>
 B(1) × B(2) = B(3) を満たすようにできるなら Yes 、そうでないなら No と出力せよ。
 */

namespace A_Shuffled_Equation {
    internal class Program {
        static void Main(string[] args) {
            var wSortedNumbers = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).OrderByDescending(x => x).ToArray();
            Console.WriteLine(wSortedNumbers[0] == wSortedNumbers[1] * wSortedNumbers[2] ? "Yes" : "No");
        }
    }
}
