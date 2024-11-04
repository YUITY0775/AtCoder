using System;

/*
 <問題>
 H 行 W 列のマス目があります。マス目の上から i 番目、左から j 番目のマスをマス (i, j) と表記します。
 マス (i, j) は S(i, j) が . のとき空きマスであり、# のとき障害物があります。
 ある空きマスを出発し、上下左右に隣接するマスへの移動を K 回行う方法であって、障害物のあるマスを通らず、同じマスを 
 2 回以上通らないようなものの個数を数えてください。

 具体的には、長さ K + 1 の列 ((i(0), j(0)), (i(1), j(1)), …, (i(K), j(K)) であって、以下を満たすものの個数を数えてください。

 ・各 0 ≤ k ≤ K について、 1 ≤ i(k) ≤ H, 1 ≤ j(k) ≤ W かつ S(ik, jk)  は . である
 ・各 0 ≤ k ≤ K − 1 について、|i(k + 1) - i(k)| + |j(k + 1) - j(k)| = 1
 ・各 0 ≤ k < l ≤ K について、 (i(k), j(k)) != (i(l), j(l)) である

 <制約>
 ・1 ≤ H, W ≤ 10
 ・1 ≤ K ≤ 11
 ・H, W, K は整数
 ・S(i, j) は . または #
 ・空きマスが少なくとも 1 つ存在する

 <入力>
 H W K
 S(1, 1)S(1, 2)…S(1, W)
 S(2, 1)S(2, 2)…S(2, W)
 …
 S(H, 1)S(H, 2)…S(H, W)

 <出力>
 答えを出力せよ。
 */

namespace D_Count_Simple_Paths {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wLine = Console.ReadLine().Split(' ');
            (int wRowsCount, int wColumnsCount, int wMoveCount) = (int.Parse(wLine[0]), int.Parse(wLine[1]), int.Parse(wLine[2]));

            bool[,] wField = new bool[wRowsCount, wColumnsCount];

            // 通過不可のマスをtrueとする
            for (int i = 0; i < wRowsCount; i++) {
                var wRow = Console.ReadLine();
                for (int j = 0; j < wColumnsCount; j++) {
                    if (wRow[j] == '.') continue;
                    wField[i, j] = true;
                }
            }

            // 各マスをスタート地点としたときの条件を満たす経路の個数を深さ優先探索で求める
            long wRouteCount = 0;
            for (int i = 0; i < wRowsCount; i++) {
                for (int j = 0; j < wColumnsCount; j++) {
                    if (wField[i, j]) continue;
                    wRouteCount += GetRouteCount(wField, i, j, wMoveCount);
                }
            }

            // 出力
            Console.WriteLine(wRouteCount);
        }

        // 移動方向を固定の配列として定義
        static readonly (int Row, int Column)[] FDirections = { (0, 1), (1, 0), (0, -1), (-1, 0) };

        /// <summary>
        /// 条件を満たす経路の数を深さ優先探索により再帰的に求めます。
        /// </summary>
        /// <param name="vField">マップ</param>
        /// <param name="vRow">現在位置の行番号</param>
        /// <param name="vColumn">現在位置の列番号</param>
        /// <param name="vRestMoveCount">残りの移動回数</param>
        /// <returns></returns>
        static int GetRouteCount(bool[,] vField, int vRow, int vColumn, int vRestMoveCount) {

            // 移動回数条件を満たしたら 1 を返す。
            if (vRestMoveCount == 0) return 1;

            int wRouteCount = 0;
            // (vRow, vColumn) を通過済に変更する
            vField[vRow, vColumn] = true;

            foreach (var wSquare in FDirections) {
                int wDestRow = vRow + wSquare.Row;
                int wDestColumn = vColumn + wSquare.Column;
                if (0 <= wDestColumn && wDestColumn < vField.GetLength(1) &&
                    0 <= wDestRow && wDestRow < vField.GetLength(0) && !vField[wDestRow, wDestColumn]) {
                    wRouteCount += GetRouteCount(vField, wDestRow, wDestColumn, vRestMoveCount - 1);
                }
            }
            // (vRow, vCloumn) からの移動先をすべてチェックし終えたら、他の経路に影響を与えないよう未通過に戻す
            vField[vRow, vColumn] = false;

            return wRouteCount;
        }
    }
}
