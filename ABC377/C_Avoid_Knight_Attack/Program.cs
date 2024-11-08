using System;
using System.Collections.Generic;

/*
 <問題>
 縦 N マス、横 N マスの N^2 マスからなるマス目があります。 上から i 行目 (1 ≤ i ≤ N) 、
 左から j 列目 (1 ≤ j ≤ N) のマスをマス (i, j) と呼ぶことにします。
 それぞれのマスは、空マスであるかコマが置かれているかのどちらかです。
 マス目には合計で M 個のコマが置かれており、k 番目 (1 ≤ k ≤ M) のコマはマス (a(k), b(k)) に置かれています。
 あなたは、すでに置かれているどのコマにも取られないように、いずれかの空マスに自分のコマを置きたいです。
 マス (i, j) に置かれているコマは、次のどれかの条件を満たすコマを取ることができます。

 ・マス (i + 2, j + 1) に置かれている
 ・マス (i + 1, j + 2) に置かれている
 ・マス (i − 1, j + 2) に置かれている
 ・マス (i − 2, j + 1) に置かれている
 ・マス (i − 2, j − 1) に置かれている
 ・マス (i − 1, j − 2) に置かれている
 ・マス (i + 1, j − 2) に置かれている
 ・マス (i + 2, j − 1) に置かれている

 ただし、存在しないマスについての条件は常に満たされないものとします。
 あなたがコマを置くことができるマスがいくつあるか求めてください。

 <制約>
 ・1 ≤ N ≤ 10^9 
 ・1 ≤ M ≤ 2×10^5
 ・1 ≤ a(k) ≤ N, 1 ≤ b(k) ≤ N (1 ≤ k ≤ M)
 ・(a(k), b(k)) != (a(l), b(l)) (1 ≤ k < l ≤ M)
 ・入力はすべて整数

 <入力>
 N M
 a(1) b(1)
 a(2) b(2)
 …
 a(M) b(M)

 <出力>
 すでに置かれているコマに取られずに自分のコマを置くことができる空マスの個数を出力せよ。
 */

namespace C_Avoid_Knight_Attack {
    internal class Program {

        static readonly (int X, int Y)[] FDirection = { (1, -2), (2, -1), (2, 1), (1, 2), (-1, 2), (-2, 1), (-2, -1), (-1, -2) };
        static void Main(string[] args) {

            // 入力受付
            var wInputLine = Console.ReadLine().Split(' ');
            (long wMatrixOrder, int wPiecesCount) = (long.Parse(wInputLine[0]), int.Parse(wInputLine[1]));

            var wFilledCells = new HashSet<(int X, int Y)>();
            var wPieces = new List<(int X, int Y)>();
            for (int i = 0; i < wPiecesCount; i++) {
                var wLine = Console.ReadLine().Split(' ');
                wFilledCells.Add((int.Parse(wLine[0]) - 1, int.Parse(wLine[1]) - 1));
                wPieces.Add((int.Parse(wLine[0]) - 1, int.Parse(wLine[1]) - 1));
            }

            // 各駒が移動できる座標を HashSet に追加する
            foreach ((int X, int Y) wPiece in wPieces) {
                foreach ((int X, int Y) in FDirection) {
                    int wXCoordinate = wPiece.X + X;
                    int wYCoordinate = wPiece.Y + Y;
                    if (0 <= wXCoordinate && wXCoordinate < wMatrixOrder && 0 <= wYCoordinate && wYCoordinate < wMatrixOrder) {
                        wFilledCells.Add((wXCoordinate, wYCoordinate));
                    }
                }
            }

            // 出力
            Console.WriteLine(wMatrixOrder * wMatrixOrder - (long)wFilledCells.Count);
        }
    }
}
