using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc431/tasks/abc431_e
 */

namespace E_ReflectionOnGrid {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int casesCount = int.Parse(Console.ReadLine());
            var testCases = new List<TestCase>();
            var strategy = new BFSStrategy();
            for (int i = 0; i < casesCount; i++) {
                testCases.Add(new TestCase(new TestCaseReader(), strategy));
            }

            // テストケースの実行
            var answers = new List<long>();
            foreach (var testCase in testCases) {
                answers.Add(testCase.Execute());
            }

            // 出力処理
            Console.WriteLine(string.Join(Environment.NewLine, answers));
        }
    }

    #region テストケース関連

    /// <summary>
    /// テストケース
    /// </summary>
    internal class TestCase {

        /// <summary>
        /// テストケース読み取り機能
        /// </summary>
        private readonly ITestCaseReader _reader;

        private readonly IExecuteStrategy _strategy;

        /// <summary>
        /// 鏡の部屋
        /// </summary>
        private readonly MirrorRoom _room;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCase(ITestCaseReader reader, IExecuteStrategy strategy) {
            _reader = reader;
            _reader.Read();
            _room = new MirrorRoom(_reader.Sections);
            _strategy = strategy;
        }

        /// <summary>
        /// テストケースを実行して結果を返す
        /// </summary>
        public long Execute() => _strategy.Execute(_room);
    }

    /// <summary>
    /// テストケース読み取り機能
    /// </summary>
    interface ITestCaseReader {

        /// <summary>
        /// 区画の配列
        /// </summary>
        Section[][] Sections { get; }

        /// <summary>
        /// 読み取り処理
        /// </summary>
        void Read();
    }

    /// <summary>
    /// テストケース読み取り機能
    /// </summary>
    internal class TestCaseReader : ITestCaseReader {

        /// <summary>
        /// 区画の配列
        /// </summary>
        public Section[][] Sections { get; private set; }

        /// <summary>
        /// 読み取り処理
        /// </summary>
        public void Read() {
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            (int rowsCount, int colsCount) = (inputs[0], inputs[1]);
            this.Sections = new Section[rowsCount][];
            for (int i = 0; i < rowsCount; i++) {
                this.Sections[i] = Console.ReadLine().Select(x => new Section(x)).ToArray();
            }
        }
    }

    #endregion

    #region 実行戦略関連

    /// <summary>
    /// テストケースの実行戦略
    /// </summary>
    interface IExecuteStrategy {

        /// <summary>
        /// 戦略を実行する
        /// </summary>
        long Execute(MirrorRoom room);
    }

    /// <summary>
    /// 01BFSで実行する戦略
    /// </summary>
    internal class BFSStrategy : IExecuteStrategy {

        /// <summary>
        /// インデックスから方向ベクトルへの変換用マップ（上：0, 下：1, 左：2, 右：3）
        ///     ０
        ///  ２□３
        ///     １
        /// </summary>
        private static readonly Dictionary<int, (int X, int Y)> INDEX_TO_DIRECTION = new Dictionary<int, (int, int)> {
            { 0, (1, 0) },
            { 1, (-1, 0) },
            { 2, (0, 1) },
            { 3, (0, -1) }
        };

        /// <summary>
        /// 戦略を実行する
        /// </summary>
        public long Execute(MirrorRoom room) {

            // 区画 x, y に方向 d から到達するまでの最小コストを dp 配列で管理する（dp[x][y][d]）
            var dp = new long[room.XLength][][];

            // 最大コストで初期化する
            long maxCost = room.XLength * room.YLength;
            for (int x = 0; x < room.XLength; x++) {
                dp[x] = new long[room.YLength][];
                for (int y = 0; y < room.YLength; y++) {
                    dp[x][y] = Enumerable.Repeat(maxCost, INDEX_TO_DIRECTION.Count).ToArray();
                }
            }

            dp[0][0][2] = 0;
            var deque = new LinkedList<(int X, int Y, int From)>();
            deque.AddFirst((0, 0, 2));
            while (deque.Count > 0) {
                var current = deque.First.Value;
                deque.RemoveFirst();

                // 区画から出ていく方向ごとに処理する
                foreach (var direction in INDEX_TO_DIRECTION) {
                    // 光が入ってきた方向に出ていくことはあり得ないので処理しない
                    int nextFrom = direction.Key;
                    if ((nextFrom ^ current.From) == 1) continue;

                    // 部屋外に出てしまう場合は処理しない
                    int nextX = current.X + direction.Value.X;
                    int nextY = current.Y + direction.Value.Y;
                    if (nextX < 0 || room.XLength <= nextX || nextY < 0 || room.YLength <= nextY) continue;

                    // コストが必要な場合と不要な場合それぞれ考慮して処理する
                    bool isNeedCost = this.IsNeedCost(room.GetMirrorType(current.X, current.Y), nextFrom, current.From);
                    long nextCost = dp[current.X][current.Y][current.From] + (isNeedCost ? 1 : 0);

                    if (dp[nextX][nextY][nextFrom] <= nextCost) continue;

                    // コストが必要な場合は両端キューの後ろに、不要な場合は前に挿入する
                    dp[nextX][nextY][nextFrom] = nextCost;
                    if (isNeedCost) {
                        deque.AddLast((nextX, nextY, nextFrom));
                    } else {
                        deque.AddFirst((nextX, nextY, nextFrom));
                    }
                }
            }
            // 右下の区画に上から入った場合と左から入った場合でコストが小さいほうを返す
            var mirrorType = room.GetMirrorType(room.XLength - 1, room.YLength - 1);
            return Math.Min(dp[room.XLength - 1][room.YLength - 1][0] + (this.IsNeedCost(mirrorType, 2, 0) ? 1 : 0), 
                dp[room.XLength - 1][room.YLength - 1][2] + (this.IsNeedCost(mirrorType, 2, 2) ? 1 : 0));
        }

        /// <summary>
        /// 光が区画から次の区画へ移動するためにコストが必要か否かを判定する
        /// </summary>
        private bool IsNeedCost(MirrorType mirrorType, int to, int from) {
            switch (mirrorType) {
                case MirrorType.None:
                    return (to ^ from) != 0;
                case MirrorType.Slash:
                    return (to ^ from) != 3;
                default:
                    return (to ^ from) != 2;
            }
        }
    }

    #endregion

    #region 鏡の部屋関連

    /// <summary>
    /// 鏡の部屋
    /// </summary>
    internal class MirrorRoom {

        /// <summary>
        /// 区画の配列
        /// </summary>
        private readonly Section[][] _room;

        /// <summary>
        /// 鏡の部屋のX方向の区画の数
        /// </summary>
        public int XLength => _room.Length;

        /// <summary>
        /// 鏡の部屋のY方向の区画の数
        /// </summary>
        public int YLength => _room[0].Length;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MirrorRoom(Section[][] room) {
            _room = room;
        }

        /// <summary>
        /// 指定した区画の鏡タイプを取得する
        /// </summary>
        public MirrorType GetMirrorType(int x, int y) => _room[x][y].Type;
    }

    /// <summary>
    /// 区画
    /// </summary>
    internal class Section {

        /// <summary>
        /// 鏡の配置タイプ
        /// </summary>
        public MirrorType Type;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Section(char type) {
            switch (type) {
                case 'A':
                    Type = MirrorType.None;
                    break;
                case 'B':
                    Type = MirrorType.BackSlash;
                    break;
                default:
                    Type = MirrorType.Slash;
                    break;
            }
        }
    }

    /// <summary>
    /// 鏡の配置タイプ
    /// </summary>
    internal enum MirrorType {
        None,          // 鏡なし
        Slash,          // 斜め（右下方向）配置
        BackSlash,   // 斜め（左下方向）配置
    }

    #endregion
}
