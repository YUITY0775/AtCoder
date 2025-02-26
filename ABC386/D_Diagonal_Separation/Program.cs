using System;
using System.Collections.Generic;

/*
 <問題>
 N 行 N 列のグリッドがあります。高橋君は以下の条件を満たすように各マスを黒または白のいずれかに塗り分けたいと考えています。

 ・すべての行について以下の条件が成り立つ。
     ・ある整数 i (0 ≤ i ≤ N) が存在して、その行の左から i 個のマスは黒、それ以外は白で塗られている。
 ・すべての列について以下の条件が成り立つ。
     ・ある整数 i (0 ≤ i ≤ N) が存在して、その列の上から i 個のマスは黒、それ以外は白で塗られている。

 M 個のマスがすでに塗られています。そのうち i 個目は上から X(i) 行目、左から Y(i) 列目のマスで、C(i) が B のとき黒で、
 W のとき白で塗られています。

 高橋君はまだ塗られていない残りの N^2 − M 個のマスの色をうまく決めることで条件を満たすことができるか判定してください。

 <制約>
 ・1 ≤ N ≤ 10^9
 ・1 ≤ M ≤ min(N^2, 2×10^5)
 ・1 ≤ X(i), Y(i) ≤ N
 ・(X(i), Y(i)) != (X(j), Y(j)) (i != j)
 ・C(i) は B または W
 ・入力される数値はすべて整数

 <入力>
 N M
 X(1) Y(1) C(1)
 …
 X(M) Y(M) C(M)

 <出力>
 条件を満たすことができるとき Yes を、そうでないとき No を出力せよ。
 */

namespace D_Diagonal_Separation {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wMapInfo = Console.ReadLine().Split(' ');
            (int wRowsCount, int wPaintedCellsCount) = (int.Parse(wMapInfo[0]), int.Parse(wMapInfo[1]));

            // 出力
            Console.WriteLine(CanPaintGrid(wPaintedCellsCount) ? "Yes" : "No");
        }

        /// <summary>
        /// 列番号をキーとして、要素 1 に黒マスの最大行番号、要素 2 に白マスの最小行番号を保持する配列を持つ連想配列
        /// </summary>
        static SortedDictionary<int, (int BlackMaxIndex, int WhiteMinIndex)> FColumnsInfo = new SortedDictionary<int, (int, int)>();

        /// <summary>
        /// 条件を満たすようにグリッドを塗ることができるか否かを判定する
        /// </summary>
        /// <param name="vPaintedCellsCount">既に塗られたセルの数</param>
        /// <returns>条件を満たすようにグリッドを塗ることができるか否か</returns>
        static bool CanPaintGrid(int vPaintedCellsCount) {
            CreateColumnsInfo(vPaintedCellsCount);
            int wWhiteIndex = int.MaxValue;

            foreach (var wColumnInfo in FColumnsInfo.Values) {
                wWhiteIndex = Math.Min(wWhiteIndex, wColumnInfo.WhiteMinIndex);
                if (wWhiteIndex <= wColumnInfo.BlackMaxIndex) return false;
            }
            return true;
        }

        /// <summary>
        /// 既に塗られたグリッドのセルの情報を格納した連想配列を作成する
        /// </summary>
        /// <param name="vPaintedCellsCount">既に塗られたセルの数</param>
        static void CreateColumnsInfo(int vPaintedCellsCount) {
            for (int i = 0; i < vPaintedCellsCount; i++) {
                var wReadLine = Console.ReadLine().Split(' ');
                (int wRow, int wColumn, bool wIsBlack) = (int.Parse(wReadLine[0]), int.Parse(wReadLine[1]), wReadLine[2] == "B");

                if (!FColumnsInfo.TryGetValue(wColumn, out var wColumnInfo)) {
                    FColumnsInfo.Add(wColumn, wIsBlack ? (wRow, int.MaxValue) : (int.MinValue, wRow));
                    continue;
                }
                
                if (wIsBlack) {
                    FColumnsInfo[wColumn] = (Math.Max(wRow, wColumnInfo.BlackMaxIndex), wColumnInfo.WhiteMinIndex);
                } else {
                    FColumnsInfo[wColumn] = (wColumnInfo.BlackMaxIndex, Math.Min(wRow, wColumnInfo.WhiteMinIndex));
                }
            }
        }
    }
}
