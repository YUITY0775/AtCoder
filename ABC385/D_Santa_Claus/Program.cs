using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 2次元平面上の N 点 (X(1), Y(1)), …, (X(N), Y(N)) に家が建っています。
 最初、点 (S(x), S(y)) にサンタクロースがいます。
 サンタクロースは列 (D(1), C(1)), …, (D(M), C(M)) に従って以下の行動を行います。

 ・i = 1 , 2, …, M の順に以下のように移動する。
   ・現在サンタクロースがいる点を (x, y) とする。
     ・D(i) が U なら、(x, y) から (x, y + C(i)) に直線で移動する。
     ・D(i) が D なら、(x, y) から (x, y − C(i)) に直線で移動する。
     ・D(i) が L なら、(x, y) から (x − C(i), y) に直線で移動する。
     ・D(i) が R なら、(x, y) から (x + C(i), y) に直線で移動する。

 行動を終えたあとにサンタクロースがいる点と、行動により通過または到達した家の数を求めてください。
 ただし、同じ家を複数回通過または到達してもそれらは重複して数えません。

 <制約>
 ・1 ≤ N ≤ 2×10^5
 ・1 ≤ M ≤ 2×10^5
 ・−10^9 ≤ X(i), Y(i) ≤ 10^9
 ・(X(i), Y(i)) は相異なる
 ・−10^9 ≤ S(x) ,S(y) ≤ 10^9
 ・(S(x), S(y)) に家は建っていない
 ・D(i) は U, D, L, R のいずれかである
 ・1 ≤ C(i) ≤ 10^9
 ・与えられる数値は全て整数である

 <入力>
 N M S(x) S(y)
 X(1) Y(1)
 …
 X(N) Y(N)
 D(1) C(1)
 …
 D(M) C(M)

 <出力>
 行動を終えたあとサンタクロースがいる点を (X, Y)、行動により通過または到達した家の数を C とするとき、
 X, Y, C をこの順に空白区切りで出力せよ。
 */

namespace D_Santa_Claus {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wLineInfo = Console.ReadLine().Split(' ').ToArray();

            var wHomesCoordinates = new HashSet<(long X, long Y)>();
            for (int i = 0; i < int.Parse(wLineInfo[0]); i++) {
                var wCoordinate = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
                wHomesCoordinates.Add((wCoordinate[0], wCoordinate[1]));
            }
            var wCommands = new (char Command, int Times)[int.Parse(wLineInfo[1])];
            for (int i = 0; i < wCommands.Length; i++) {
                var wCommand = Console.ReadLine().Split(' ');
                wCommands[i] = (wCommand[0][0], int.Parse(wCommand[1]));
            }

            var wSantaClaus = new SantaClaus(long.Parse(wLineInfo[2]), long.Parse(wLineInfo[3]), wHomesCoordinates);
            long wPrevHomesCount = wSantaClaus.CountNonVisitedHomes();

            // 移動処理
            for (int i = 0; i < wCommands.Length; i++) {
                wSantaClaus.MoveTo(wCommands[i]);
            }

            // 出力
            Console.WriteLine($"{wSantaClaus.X} {wSantaClaus.Y} {wPrevHomesCount - wSantaClaus.CountNonVisitedHomes()}");
        }
    }

    /// <summary>
    /// サンタクロース
    /// </summary>
    internal class SantaClaus {

        /// <summary>
        /// サンタの移動コマンドと移動方向
        /// </summary>
        private static Dictionary<char, (int X, int Y)> FCommandToDirection = new Dictionary<char, (int, int)> {
            {'U', (0, 1) },
            {'D', (0, -1) },
            {'L', (-1, 0) },
            {'R', (1, 0) },
        };

        /// <summary>
        /// 計算のために X 座標方向に Y 座標をまとめて管理しておく
        /// </summary>
        private static Dictionary<long, SortedSet<long>> FXToYCoordinates = new Dictionary<long, SortedSet<long>>();

        /// <summary>
        /// 計算のために Y 座標方向に X 座標をまとめて管理しておく
        /// </summary>
        private static Dictionary<long, SortedSet<long>> FYToXCoordinates = new Dictionary<long, SortedSet<long>>();

        /// <summary>
        /// サンタの担当地区にある家の座標(まだ訪れていない家のみ)
        /// </summary>
        private HashSet<(long X, long Y)> FNonVisitedHomes;

        /// <summary>
        /// サンタがいるマスの X 座標
        /// </summary>
        public long X { get; private set; }

        /// <summary>
        /// サンタがいるマスの Y 座標
        /// </summary>
        public long Y { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vX">サンタがいるマスの行番号</param>
        /// <param name="vY">サンタがいるマスの列番号</param>
        public SantaClaus(long vX, long vY, HashSet<(long, long)> vHomes) {
            this.X = vX;
            this.Y = vY;
            FNonVisitedHomes = vHomes;
            SetCoordinates();
        }

        /// <summary>
        /// コンストラクタ呼び出し時に、計算用のための座標管理マップをセットする
        /// </summary>
        private void SetCoordinates() {
            foreach (var wHome in this.FNonVisitedHomes) {
                if (FXToYCoordinates.TryGetValue(wHome.X, out SortedSet<long> wYCoordinates)) {
                    wYCoordinates.Add(wHome.Y);
                } else {
                    FXToYCoordinates.Add(wHome.X, new SortedSet<long> { wHome.Y });
                }

                if (FYToXCoordinates.TryGetValue(wHome.Y, out SortedSet<long> wXCoordinates)) {
                    wXCoordinates.Add(wHome.X);
                } else {
                    FYToXCoordinates.Add(wHome.Y, new SortedSet<long> { wHome.X });
                }
            }
        }

        /// <summary>
        /// サンタの担当地区のうち、まだ訪れていない家の数を返す
        /// </summary>
        /// <returns></returns>
        public long CountNonVisitedHomes() => FNonVisitedHomes.Count;

        /// <summary>
        /// コマンドに応じて移動処理を行う
        /// </summary>
        /// <param name="vCommand">コマンド</param>
        public void MoveTo((char Command, int Times) vCommand) {
            var wDirection = FCommandToDirection[vCommand.Command];
            (long wPrevX, long wPrevY) = (this.X, this.Y);
            this.X += wDirection.X * vCommand.Times;
            this.Y += wDirection.Y * vCommand.Times;

            DeleteHome(wPrevX, wPrevY);
        }

        /// <summary>
        /// サンタが既に訪れた家を FNonVisitedHomes から削除する
        /// </summary>
        /// <param name="vPrevX">移動前の X 座標</param>
        /// <param name="vPrevY">移動前の Y 座標</param>
        private void DeleteHome(long vPrevX, long vPrevY) {
            var wTargetHomes = new HashSet<(long X, long Y)>();
            if (vPrevX != this.X) {
                if (!FYToXCoordinates.TryGetValue(vPrevY, out SortedSet<long> wXCoordinates)) return;
                foreach (var wXCoordinate in wXCoordinates.GetViewBetween(Math.Min(this.X, vPrevX), Math.Max(this.X, vPrevX))) {
                    wTargetHomes.Add((wXCoordinate, this.Y));
                }
            } else {
                if (!FXToYCoordinates.TryGetValue(vPrevX, out SortedSet<long> wYCoordinates)) return;
                foreach (var wYCoordinate in wYCoordinates.GetViewBetween(Math.Min(this.Y, vPrevY), Math.Max(this.Y, vPrevY))) {
                    wTargetHomes.Add((this.X, wYCoordinate));
                }
            }
            foreach (var wHome in wTargetHomes) {
                this.FNonVisitedHomes.Remove(wHome);
                FYToXCoordinates[wHome.Y].Remove(wHome.X);
                FXToYCoordinates[wHome.X].Remove(wHome.Y);
            }
        }
    }
}