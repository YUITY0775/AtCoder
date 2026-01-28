using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc442/tasks/abc442_e
 */

namespace E_LaserTakahashi {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            (int monstersCount, int queriesCount) = (inputs[0], inputs[1]);

            // モンスターの生成
            var monsters = new Monster[monstersCount];
            for (int i = 0; i < monstersCount; i++) {
                var coords = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                monsters[i] = new Monster(coords[0], coords[1], i + 1);
            }
            var manager = new MonsterManager(monsters, new PolarAngleComparer());

            // クエリ処理
            var answers = new List<int>();
            for (int i = 0; i < queriesCount; i++) {
                var queryInfo = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var leftMonster = manager.GetMonsterById(queryInfo[0]);
                var rightMonster = manager.GetMonsterById(queryInfo[1]);
                answers.Add(manager.CountBetween(leftMonster, rightMonster));
            }

            // 出力処理
            Console.WriteLine(string.Join(Environment.NewLine, answers));
        }
    }

    #region 偏角ソート関連

    /// <summary>
    /// 偏角での比較
    /// </summary>
    internal sealed class PolarAngleComparer : IComparer<Monster> {
        public int Compare(Monster monsterA, Monster monsterB) {
            // 上半平面と下半平面では上半平面が前扱いとする
            if (monsterA.IsUpper ^ monsterB.IsUpper) return monsterA.IsUpper ? -1 : 1;

            // 外積を用いて偏角の大小を比較する（偏角が同じ大きさのものの並び順は保証しない）
            long arg = (long)monsterA.X * monsterB.Y - (long)monsterA.Y * monsterB.X;
            if (arg == 0) return 0;
            return arg > 0 ? -1 : 1;
        }
    }

    #endregion

    #region モンスター関連

    /// <summary>
    /// モンスター
    /// </summary>
    internal class Monster {

        /// <summary>
        /// X座標
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y座標
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// モンスターID
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// 上半平面にいるか否か
        /// </summary>
        /// <remarks>上半平面：[0, 180), 下半平面：[180, 360)</remarks>
        public bool IsUpper => this.Y > 0 || (this.Y == 0 && this.X > 0);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Monster(int x, int y, int id) {
            this.X = x;
            this.Y = y;
            this.ID = id;
        }
    }

    /// <summary>
    /// モンスターの管理
    /// </summary>
    internal class MonsterManager {

        /// <summary>
        /// モンスターの比較方法
        /// </summary>
        private readonly IComparer<Monster> _comparer;

        /// <summary>
        /// モンスター一覧（二分探索のため昇順前提）
        /// </summary>
        private readonly IReadOnlyList<Monster> _monsters;

        /// <summary>
        /// モンスターIDからモンスターを検索するための連想配列
        /// </summary>
        private readonly IReadOnlyDictionary<int, Monster> _idToMonster;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonsterManager(IEnumerable<Monster> monsters, IComparer<Monster> comparer) {
            _comparer = comparer;
            _monsters = monsters.OrderBy(x => x, _comparer).ToList().AsReadOnly();
            var map = new Dictionary<int, Monster>();
            for (int i = 0; i < _monsters.Count; i++) {
                var monster = _monsters[i];
                map.Add(monster.ID, monster);
            }
            _idToMonster = map;
        }

        /// <summary>
        /// モンスターIDから該当するモンスターを取得する
        /// </summary>
        public Monster GetMonsterById(int id) => _idToMonster[id];

        /// <summary>
        /// 指定したモンスター間に存在するモンスター数を返す
        /// </summary>
        public int CountBetween(Monster left, Monster right) {
            int leftIndex = this.SearchBoundaryIndex(left, isLowerBoundary: false);
            int rightIndex = this.SearchBoundaryIndex(right, isLowerBoundary: true);
            int count = leftIndex - rightIndex + 1;
            if (count <= 0) {
                count += _monsters.Count;
            }
            return count;
        }

        /// <summary>
        /// 指定したモンスターと同じ偏角のモンスターの境界を求める
        /// </summary>
        private int SearchBoundaryIndex(Monster target, bool isLowerBoundary) {
            (int left, int right) = (0, _monsters.Count);
            while (left < right) {
                int mid = (left + right) / 2;
                int compare = _comparer.Compare(_monsters[mid], target);
                if (compare < 0 || (!isLowerBoundary && compare == 0)) {
                    left = mid + 1;
                } else {
                    right = mid;
                }
            }
            return isLowerBoundary ? left : left - 1;
        }
    }

    #endregion
}
