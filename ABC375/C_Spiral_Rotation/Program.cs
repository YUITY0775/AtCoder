using System;
using System.Collections.Generic;
using System.Text;

/*
 <問題>
 N 行 N 列のグリッドが与えられます。ここで、N は偶数です。グリッドの上から i 行目、左から j 列目のマスをマス (i, j) と表記します。
 グリッドの各マスは黒か白のいずれかで塗られており、A(i, j) = # のときマス (i, j) は黒、A(i, j) = . のときマス (i, j) は白で塗られています。
 i = 1, 2, …, N/2 の順に以下の操作を行った後のグリッドの各マスの色を求めてください。

 ・i 以上 N + 1 − i 以下の整数 x, y について、マス (y, N + 1 − x) の色をマス (x, y) の色で置き換える。
   この置き換えは条件を満たすすべての整数 x, y について同時に行う。

 <制約>
 ・N は 2 以上 3000 以下の偶数
 ・A(i, j) は # または .

 <入力>
 N
 A(1, 1) A(1, 2) … A(1, N)
 A(2, 1) A(2, 2) … A(2, N)
 …
 A(N, 1) A(N, 2) … A(N, N)

 <出力>
 すべての操作を終えた後、マス (i, j) の色が黒であるとき B(i, j) = #、マス (i, j) の色が白であるとき 
 B(i, j) = . として以下の形式で出力せよ。

 B(1, 1) B(1, 2) … B(1, N)
 B(2, 1) B(2, 2) … B(2, N)
 …
 B(N, 1) B(N, 2) … B(N, N)
 */

namespace C_Spiral_Rotation {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wMatrixOrder = int.Parse(Console.ReadLine());
            var wMarks = new List<char>();
            for (int i = 1; i <= wMatrixOrder; i++) {
                var wLine = Console.ReadLine();
                wMarks.AddRange(wLine.ToCharArray());
            }
            var wRemakedMarks = new List<char>();
            for (int i = 1; i <= wMatrixOrder; i++) {
                for (int j = 1; j <= wMatrixOrder; j++) {
                    var wPosition = Math.Min(Math.Min(i, j), Math.Min(wMatrixOrder - i + 1, wMatrixOrder - j + 1));
                    var p = i;
                    var q = j;
                    for (int k = 0; k < wPosition % 4; k++) {
                        var wPreviousP = p;
                        p = wMatrixOrder + 1 - q;
                        q = wPreviousP;
                    }
                    wRemakedMarks.Add(wMarks[(p - 1) * wMatrixOrder + (q - 1)]);
                }
            }
            var wAnswer = new StringBuilder();
            for (int i = 1; i <= wRemakedMarks.Count; i++) {
                wAnswer.Append(wRemakedMarks[i - 1]);
                if(i % wMatrixOrder == 0) wAnswer.Append(Environment.NewLine);
            }
            Console.WriteLine(wAnswer.ToString());
        }
    }
}
