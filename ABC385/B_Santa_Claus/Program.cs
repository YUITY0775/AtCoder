using System;
using System.Collections.Generic;

/*
 <問題>
 縦 H 行横 W 列のマス目があります。上から i 行目、左から j 列目のマスをマス (i, j) と表します。
 マス (i, j) は S(i, j) が # のとき通行不可能、. のとき通行可能であり家が建っていない、@ のとき通行可能であり
 家が建っていることを表します。
 最初、マス (X, Y) にサンタクロースがいます。サンタクロースは文字列 T に従って以下の行動を行います。

 ・文字列 T の長さを ∣T∣ とする。 i = 1, 2, …, ∣T∣ の順に以下のように移動する。
   ・現在サンタクロースがいるマスを (x, y) とする。
     ・T(i) が U かつマス (x − 1, y) が通行可能ならマス (x − 1, y) に移動する。
     ・T(i) が D かつマス (x + 1, y) が通行可能ならマス (x + 1, y) に移動する。
     ・T(i) が L かつマス (x, y − 1) が通行可能ならマス (x, y − 1) に移動する。
     ・T(i) が R かつマス (x, y + 1) が通行可能ならマス (x, y + 1) に移動する。
     ・それ以外の場合、マス (x, y) に留まる。

 行動を終えたあとにサンタクロースがいるマスと、行動により通過または到達した家の数を求めてください。
 ただし、同じ家を複数回通過または到達してもそれらは重複して数えません。

 <制約>
 ・3 ≤ H, W ≤ 100
 ・1 ≤ X ≤ H
 ・1 ≤ Y ≤ W
 ・与えられる数値は全て整数である
 ・S(i, j) は #, ., @ のいずれか
 ・全ての 1 ≤ i ≤ H について S(i, 1), S(i, W) は #
 ・全ての 1 ≤ j ≤ W について S(1, j), S(H, j) は #
 ・S(X, Y) = .
 ・T は U, D, L, R のいずれかからなる長さ 1 以上 10^4 以下の文字列

 <入力>
 H W X Y
 S(1, 1) S(1, 2) … S(1, W)
 …
 S(H, 1) S(H, 2) … S(H, W)
 T

 <出力>
 行動を終えたあとサンタクロースがいるマスを (X, Y)、行動により通過または到達した家の数を C とするとき、
 X, Y, C をこの順に空白区切りで出力せよ。
 */

namespace B_Santa_Claus {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wCoordinatesInfo = Console.ReadLine().Split();
            int wRowsCount = int.Parse(wCoordinatesInfo[0]);
            int wColumnsCount = int.Parse(wCoordinatesInfo[1]);

            var wMap = new char[wRowsCount, wColumnsCount];
            for (int i = 0; i < wRowsCount; i++) {
                var wLine = Console.ReadLine();
                for (int j = 0; j < wColumnsCount; j++) {
                    wMap[i, j] = wLine[j];
                }
            }
            var wSantaClaus = new SantaClaus(int.Parse(wCoordinatesInfo[2]) - 1, int.Parse(wCoordinatesInfo[3]) - 1, wMap);
            string wMovingInfo = Console.ReadLine();

            // 移動処理
            var wVisitedHomes = new HashSet<(int X, int Y)>();
            for (int i = 0; i < wMovingInfo.Length; i++) {
                if (!wSantaClaus.TryMoveTo(wMovingInfo[i])) continue;
                if (wSantaClaus.Map[wSantaClaus.Row, wSantaClaus.Column] != '@') continue;
                wVisitedHomes.Add((wSantaClaus.Row, wSantaClaus.Column));
            }

            // 出力
            Console.WriteLine($"{wSantaClaus.Row + 1} {wSantaClaus.Column + 1} {wVisitedHomes.Count}");
        }
    }

    /// <summary>
    /// サンタクロース
    /// </summary>
    internal class SantaClaus {

        /// <summary>
        /// サンタの移動コマンドと移動方向
        /// </summary>
        private static Dictionary<char, (int Row, int Column)> FCommandToDirection = new Dictionary<char, (int, int)> {
            {'U', (-1, 0) },
            {'D', (1, 0) },
            {'L', (0, -1) },
            {'R', (0, 1) },
        };

        /// <summary>
        /// サンタがいるマスの行番号
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// サンタがいるマスの列番号
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// サンタの担当地区の地図
        /// </summary>
        public char[,] Map { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vRow">サンタがいるマスの行番号</param>
        /// <param name="vColumn">サンタがいる増野列番号</param>
        public SantaClaus(int vRow, int vColumn, char[,] vMap) {
            this.Row = vRow;
            this.Column = vColumn;
            this.Map = vMap;
        }

        /// <summary>
        /// コマンドに応じてサンタが移動できるか否を返す。移動できる場合は移動処理も行う。
        /// </summary>
        /// <param name="vCommand">コマンド</param>
        /// <returns>コマンドに応じて移動できたか否か</returns>
        public bool TryMoveTo(char vCommand) {
            if (!FCommandToDirection.TryGetValue(vCommand, out var wDirection)) return false;
            if (this.Row + wDirection.Row < 0 || this.Map.GetLength(0) - 1 < wDirection.Row ||
                this.Column + wDirection.Column < 0 || this.Map.GetLength(1) - 1 < wDirection.Column) return false;
            if (this.Map[this.Row + wDirection.Row, this.Column + wDirection.Column] == '#') return false;

            this.Row += wDirection.Row;
            this.Column += wDirection.Column;
            return true;
        }
    }
}
