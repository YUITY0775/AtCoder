using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc440/tasks/abc440_d
 */

namespace D_ForbiddenList {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int queriesCount = Console.ReadLine().Split(' ').Select(int.Parse).Last();
            var forbiddenSet = new ForbiddenSet(Console.ReadLine().Split(' ').Select(int.Parse));

            // クエリの実行
            var processor = new QueryProcessor(forbiddenSet);
            var answer = new List<int>();
            for (int i = 0; i < queriesCount; i++) {
                var queryInfo = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                answer.Add(processor.Solve(new Query(queryInfo[0], queryInfo[1])));
            }

            // 出力処理
            Console.WriteLine(string.Join(Environment.NewLine, answer));
        }
    }

    #region 禁止された数字の配列

    /// <summary>
    /// 禁止された数字の配列
    /// </summary>
    internal class ForbiddenSet {

        /// <summary>
        /// 禁止された数字の配列（二分探索用に昇順に並び替え & 重複削除済み）
        /// </summary>
        private readonly int[] _numbers;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ForbiddenSet(IEnumerable<int> numbers) {
            var realData = numbers.Distinct().OrderBy(x => x).ToArray();

            // 二分探索を簡易にするためデータの先頭と最後尾に番兵を用意する
            _numbers = new int[realData.Length + 2];
            _numbers[0] = 0;
            _numbers[_numbers.Length - 1] = 2 * 100000000;
            realData.CopyTo(_numbers, 1);
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        public int this[int index] => _numbers[index];

        /// <summary>
        /// 要素数
        /// </summary>
        public int Count => _numbers.Length;

        /// <summary>
        /// 禁止された数字の配列のうちキー以上の最小値のインデックスを求める
        /// </summary>
        public int SearchLowerBoundIndex(int key) => this.BinarySearch(key);

        /// <summary>
        /// 禁止された数字の配列（Aとする）のうち以下の数式を満たす t のうち最小値を求める
        /// ・(A[t] - lowerBound + 1) - (t - lowerBoundIndex + 1) ≥ count
        /// </summary>
        public int SearchUpperBoundIndex(int count, int lowerBound, int lowerBoundIndex) =>
            this.BinarySearch(count, x => _numbers[x] - lowerBound + 1 - (x - lowerBoundIndex + 1) < count);

        /// <summary>
        /// 二分探索のコア処理をおこなう
        /// </summary>
        private int BinarySearch(int key, Func<int, bool> condition = null) {
            int left = 0;
            int right = _numbers.Length - 1;
            if (condition == null) condition = x => _numbers[x] < key;
            while (left < right) {
                int mid = (right + left) / 2;
                if (condition.Invoke(mid)) {
                    left = mid + 1;
                } else {
                    right = mid;
                }
            }
            return left;
        }
    }

    #endregion

    #region クエリ関連

    /// <summary>
    /// クエリ実行役
    /// </summary>
    internal class QueryProcessor {

        /// <summary>
        /// 禁止された数字の配列
        /// </summary>
        private readonly ForbiddenSet _set;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public QueryProcessor(ForbiddenSet set) {
            _set = set;
        }

        /// <summary>
        /// クエリを実行する
        /// </summary>
        public int Solve(Query query) {

            int lowerBoundIndex = _set.SearchLowerBoundIndex(query.LowerBound);
            int upperBoundIndex = _set.SearchUpperBoundIndex(query.Count, query.LowerBound, lowerBoundIndex);
            return query.LowerBound + query.Count - 1 + (upperBoundIndex - lowerBoundIndex);
        }
    }

    /// <summary>
    /// クエリ（データのみを持つ）
    /// </summary>
    internal class Query {

        /// <summary>
        /// 基準値
        /// </summary>
        public int LowerBound { get; }

        /// <summary>
        /// 何番目を求めたいか
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Query(int lowerBound, int count) {
            this.LowerBound = lowerBound;
            this.Count = count;
        }
    }

    #endregion
}
