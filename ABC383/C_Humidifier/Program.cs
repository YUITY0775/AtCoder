using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 AtCoder社のオフィスは H 行 W 列のマス目で表されます。上から i 行目、左から j 列目のマスを (i, j) と表します。
 各マスの状態は文字 S(i, j) で表され、 S(i, j) が # のときそのマスは壁、. のときそのマスは床、H のときそのマスは加湿器が置かれた床です。
 あるマスは、ある加湿器から壁を通らず上下左右に D 回以下の移動で辿り着ける場合加湿されます。
 ここで、加湿器が置かれた床のマスは必ず加湿されていることに注意してください。
 
 加湿されている床のマスの個数を求めてください。

 <制約>
 ・1 ≤ H ≤ 1000
 ・1 ≤ W ≤ 1000
 ・0 ≤ D ≤ H×W
 ・S(i, j) は # または . または H である (1 ≤ i ≤ H, 1 ≤ j ≤ W)
 ・入力される数値は全て整数

 <入力>
 H W D
 S(1, 1) S(1, 2) … S(1, W)
 S(2, 1) S(2, 2) … S(2, W)
 …
 S(H, 1) S(H, 2) … S(H, W)

 <出力>
 答えを出力せよ。
 */

namespace C_Humidifier {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wLineInfo = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            (int wRowCount, int wColumnCount, int wDistance) = (wLineInfo[0], wLineInfo[1], wLineInfo[2]);

            var wSections= new Section[wRowCount, wColumnCount];
            for (int i = 0; i < wRowCount; i++) {
                string wRowInfo = Console.ReadLine();
                for (int j = 0; j < wColumnCount; j++) {
                    SectionKind wKind = new SectionKind();
                    switch (wRowInfo[j]) {
                        case '#': wKind = SectionKind.Wall;
                            break;
                        case '.': wKind = SectionKind.Floor;
                            break;
                        default: wKind = SectionKind.Humidifier;
                            break;
                    }
                    wSections[i, j] = new Section((i, j), wKind);
                }
            }

            // Officeクラスを利用して多始点BFSで探索する
            var wOffice = new Office();
            wOffice.SetSections(wSections);
            wOffice.BreadthFirstSearch();

            // 出力
            int wHumidifiedFloorCount = 0;
            foreach (var wSection in wOffice.GetSections()) {
                if(wSection.Distance <= wDistance) wHumidifiedFloorCount++;
            }
            Console.WriteLine(wHumidifiedFloorCount);
        }
    }

    /// <summary>
    /// オフィス
    /// </summary>
    internal class Office {

        /// <summary>
        /// 探索方向
        /// </summary>
        private static readonly (int Row, int Column)[] FDirection = new (int, int)[] { (0, 1), (1, 0), (-1, 0), (0, -1) };

        /// <summary>
        /// 区画集合
        /// </summary>
        private Section[,] FSections;

        /// <summary>
        /// 区画集合を設定する
        /// </summary>
        /// <param name="vSections">区画集合</param>
        public void SetSections(Section[,] vSections) => this.FSections = vSections;

        /// <summary>
        /// 区画集合を取得する
        /// </summary>
        /// <returns></returns>
        public Section[,] GetSections() => this.FSections;

        /// <summary>
        /// 各区画の加湿器からの最短距離を多始点BFSで探索する
        /// </summary>
        public void BreadthFirstSearch() {
            var wTargetSections = new Queue<Section>();
            for (int i = 0; i < this.FSections.GetLength(0); i++) {
                for (int j = 0; j < FSections.GetLength(1); j++) {
                    if (this.FSections[i, j].Kind != SectionKind.Humidifier) continue;
                    this.FSections[i, j].Distance = 0;
                    wTargetSections.Enqueue(this.FSections[i, j]);
                }
            }

            while (wTargetSections.Count > 0) {
                Section wTargetSection = wTargetSections.Dequeue();
                foreach (var wDirection in FDirection) {
                    int wDestinationRow = wTargetSection.Point.Row + wDirection.Row;
                    int wDestinationColumn = wTargetSection.Point.Column + wDirection.Column;
                    if (wDestinationRow < 0 || this.FSections.GetLength(0) <= wDestinationRow) continue;
                    if (wDestinationColumn < 0 || this.FSections.GetLength(1) <= wDestinationColumn) continue;
                    Section wNextTargetSection = this.FSections[wDestinationRow, wDestinationColumn];
                    if (wNextTargetSection.Kind == SectionKind.Wall || wNextTargetSection.Distance != int.MaxValue) continue;
                    wNextTargetSection.Distance = wTargetSection.Distance + 1;
                    wTargetSections.Enqueue(wNextTargetSection);
                }
            }
        }
    }

    /// <summary>
    /// 区画
    /// </summary>
    internal class Section {

        /// <summary>
        /// 座標
        /// </summary>
        public (int Row, int Column) Point { get; }

        /// <summary>
        /// 区画分類
        /// </summary>
        public SectionKind Kind { get; }

        /// <summary>
        /// 加湿器からの最短距離
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vPoint">座標</param>
        /// <param name="vKind">分類</param>
        /// <param name="vDistance">加湿器からの最短距離</param>
        public Section((int, int) vPoint, SectionKind vKind, int vDistance = int.MaxValue) {
            this.Point = vPoint;
            this.Kind = vKind;
            this.Distance = vDistance;
        }
    }

    /// <summary>
    /// 区画の分類
    /// </summary>
    internal enum SectionKind {
        Wall = 0,
        Floor,
        Humidifier
    }
}
