using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* 引用元：
 https://atcoder.jp/contests/abc454/tasks/abc454_e
 */

namespace E_LRUDMoving {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int testCaseCount = int.Parse(Console.ReadLine());
            var testCases = new TestCase[testCaseCount];
            for (int i = 0; i < testCaseCount; i++) {
                var mapInfo = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var map = new SquareMap(mapInfo[0], new Cell(mapInfo[1] - 1, mapInfo[2] - 1));
                testCases[i] = new TestCase(map);
            }

            // テストケースを処理する
            var answers = new List<string>();
            for (int i = 0; i < testCaseCount; i++) {
                answers.AddRange(testCases[i].Solve());
            }

            // 出力処理
            Console.WriteLine(string.Join(Environment.NewLine, answers));
        }
    }

    #region テストケース

    /// <summary>
    /// テストケース
    /// </summary>
    internal class TestCase {

        /// <summary>
        /// マップ
        /// </summary>
        private readonly SquareMap _map;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCase(SquareMap map) {
            _map = map;
        }

        /// <summary>
        /// テストケースを解く
        /// </summary>
        public IEnumerable<string> Solve() {
            if (_map.TryGetHamiltonianPath(out string path)) {
                yield return "Yes";
                yield return path;
            } else {
                yield return "No";
            }
        }
    }

    #endregion

    #region マップ関連

    /// <summary>
    /// マップ
    /// </summary>
    internal class SquareMap {

        /// <summary>
        /// 一辺のマスの数
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// 障害物の位置
        /// </summary>
        private readonly Cell _blocked;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SquareMap(int size, Cell blocked) {
            _size = size;
            _blocked = blocked;
        }

        /// <summary>
        /// 左上のマスから右下のマスまで、障害物を避けつつすべてのマスを一度ずつ通る経路の例を1つ返す
        /// </summary>
        public bool TryGetHamiltonianPath(out string hamiltonianPath) {
            hamiltonianPath = string.Empty;
            if (!ExistsHamiltonianPath()) return false;

            (char Right, char Down, char Left, char Up) = ('R', 'D', 'L', 'U');
            var path = new StringBuilder();
            // 障害物を考慮しなくてよい上部のルートを作成する
            int upperPatternCount = _blocked.Row / 2;
            for (int i = 0; i < upperPatternCount * 2; i++) {
                bool isRightMove = i % 2 == 0;
                path.Append(isRightMove ? Right : Left, _size - 1);
                path.Append(Down);
            }

            // 障害物を考慮する必要があるルートを作成する
            int currentColumn = 0;
            bool isUpperRow = true;
            char nextMove;
            while (true) {
                if (currentColumn >= _size - 1 && !isUpperRow) break;   // 右端かつ下段に到達したら終了する

                nextMove = currentColumn == _blocked.Column ? Right : isUpperRow ? Down : Up;
                path.Append(nextMove);
                if (nextMove != Right) {
                    isUpperRow = !isUpperRow;
                    if (currentColumn < _size - 1) path.Append(Right);
                }
                currentColumn++;
            }
            path.Append(Down);

            // 障害物を考慮しなくてよい下部のルートを作成する
            int lowerPatternCount = (_size - 1 - _blocked.Row) / 2;
            for (int i = 0; i < lowerPatternCount * 2; i++) {
                bool isRightMove = i % 2 != 0;
                path.Append(isRightMove ? Right : Left, _size - 1);
                path.Append(Down);
            }

            path.Length--;    // 最後のDownは不要なので削除する
            hamiltonianPath = path.ToString();
            return true;
        }

        /// <summary>
        /// 左上のマスから右下のマスまで、障害物を避けつつすべてのマスを一度ずつ通る経路が存在するか否かを判定する
        /// </summary>
        /// <remarks>
        /// マップを市松模様に色分けすることを考えると、移動は必ず白 → 黒 → 白 → 黒 → ... と交互に行われる。
        /// また、スタートとゴールの色は必ず同じ色になる。
        /// 一辺の長さが奇数の場合、移動回数は偶数回になるため、すべてのマスを一度ずつ通ることはできない。
        /// 一辺の長さが偶数の場合、障害物のマスがスタートとゴールと同じ色になるときはすべてのマスを一度ずつ通ることはできない。
        /// </remarks>
        private bool ExistsHamiltonianPath() {
            if (_size % 2 != 0) return false;
            if ((_blocked.Row + _blocked.Column) % 2 == 0) return false;
            return true;
        }
    }

    /// <summary>
    /// マスを表す構造体
    /// </summary>
    internal readonly struct Cell {

        /// <summary>
        /// 行番号
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// 列番号
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Cell(int row, int column) {
            this.Row = row;
            this.Column = column;
        }
    }

    #endregion
}
