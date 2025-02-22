using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 長さ N の正数列 A = (A(1), A(2), …, A(N)) が与えられます。以下で定義される長さ N の数列 B = (B(1), B(2), …, B(N)) を求めてください。

 ・i = 1, 2, …, N について、B(i) を次のように定義する：
   A(i) と等しい要素が i の直前に出現した位置を B(i) とする。そのような位置が存在しなければ B(i) = −1 とする。
   より正確には、正整数 j であって，A(i) = A (j) となる j < i が存在するならば、そのうち最大の j を B(i) とする。
   そのような j が存在しなければ B(i) = -1 とする。

 <制約>
 ・1 ≤ N ≤ 2×10^5
 ・1 ≤ A(i) ≤ 10^9 
 ・入力はすべて整数

 <入力>
 N
 A(1) A(2) … A(N)

 <出力>
 B の要素を空白区切りで 1 行に出力せよ。
 */

namespace C_Repeating {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wTermsCount = int.Parse(Console.ReadLine());
            var wSequence = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            // A(i) をキー、B(i) を値とする Dictionary で管理する
            var wIndexes = new int[wTermsCount];
            var wNumberToIndex = new Dictionary<int, int>();
            for (int i = 0; i < wTermsCount; i++) {
                wIndexes[i] = wNumberToIndex.TryGetValue(wSequence[i], out int wIndex) ? wIndex + 1 : -1;
                wNumberToIndex[wSequence[i]] = i;
            }

            // 出力
            Console.WriteLine(string.Join(" ", wIndexes));
        }
    }
}
