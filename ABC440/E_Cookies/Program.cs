using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc440/tasks/abc440_e
 */

namespace E_Cookies {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            (int variety, int selectCount, int takeCount) = (inputs[0], inputs[1], inputs[2]);
            var gourmetPoints = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // クッキー情報の用意
            var context = new CookieContext(variety, selectCount, gourmetPoints);
            var selector = new CookieSelector(context);

            // 出力処理
            var points = selector.EnumeratePoint(takeCount).ToList();
            Console.WriteLine(string.Join(Environment.NewLine, points));
        }
    }

    #region クッキー関連

    /// <summary>
    /// クッキーの選択実行役
    /// </summary>
    internal class CookieSelector {

        /// <summary>
        /// クッキーオブジェクトに共通する情報
        /// </summary>
        private readonly CookieContext _context;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CookieSelector(CookieContext context) {
            _context = context;
        }

        /// <summary>
        /// 指定された個数の美味しさポイントを降順で取り出す（ポイントの重複あり）
        /// </summary>
        public IEnumerable<long> EnumeratePoint(int take) {
            // 美味しさポイントでの降順になるように比較方法を定義
            var comparer = Comparer<CookiePattern>.Create((x, y) => y.Point.CompareTo(x.Point));

            // 優先度付きキューの用意
            int maxLength = 1 + take * (_context.Variety - 1);
            var queue = new PriorityQueue<CookiePattern>(maxLength, comparer);
            var pattern = CookiePattern.CreateInitialPattern(_context);
            queue.Enqueue(pattern);
            while (queue.Count > 0 && take > 0) {
                var current = queue.Dequeue();
                yield return current.Point;
                take--;
                foreach (var next in current.EnumerateNextPattern()) {
                    queue.Enqueue(next);
                }
            }
        }
    }

    /// <summary>
    /// クッキーオブジェクトに共通する情報
    /// </summary>
    internal class CookieContext {

        /// <summary>
        /// クッキーの種類数
        /// </summary>
        public int Variety { get; }

        /// <summary>
        /// クッキーの選択数
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// 各種クッキーの美味しさポイント（降順ソート済み）
        /// </summary>
        public IReadOnlyList<int> GourmetPoints { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CookieContext(int variety, int count, IEnumerable<int> gourmetPoints) {
            this.Variety = variety;
            this.Count = count;
            this.GourmetPoints = gourmetPoints.OrderByDescending(x => x).ToList().AsReadOnly();
        }
    }

    /// <summary>
    /// クッキーの選択パターン
    /// </summary>
    internal class CookiePattern {

        /// <summary>
        /// クッキーオブジェクトに共通する情報
        /// </summary>
        private readonly CookieContext _context;

        /// <summary>
        /// 美味しさポイント
        /// </summary>
        public long Point { get; }

        /// <summary>
        /// 遷移対象のインデックス（次の遷移を求めるために必要）
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 遷移対象のひとつ前のインデックスの値（次の遷移を求めるために必要）
        /// </summary>
        public int Previous {  get; }

        /// <summary>
        /// 遷移対象のインデックスの値（次の遷移を求めるために必要）
        /// </summary>
        public int Current { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CookiePattern(CookieContext context, long point, int index, int previous, int current) {
            _context = context;
            this.Point = point;
            this.Index = index;
            this.Previous = previous;
            this.Current = current;
        }

        /// <summary>
        /// 初期パターンの生成（美味しさポイントが最大になるパターン）
        /// </summary>
        /// <remarks>
        /// クッキーの種類数の長さの配列ではなく、クッキーの選択数の長さの配列でパターンを考慮する。
        /// 前者を選択すると重複管理が必要になるが、後者を選択することで重複管理を考慮しなくてよい。
        /// 初期パターンはすべて context.GourmetPoint[0] のクッキーを選択するときの (0, 0, …, 0) とする。
        /// </remarks>
        public static CookiePattern CreateInitialPattern(CookieContext context)
            => new CookiePattern(context, (long)context.Count * context.GourmetPoints[0], 0, context.Variety - 1, 0);

        /// <summary>
        /// 次のパターンを列挙する（重複が生まれない遷移のみ）
        /// </summary>
        public IEnumerable<CookiePattern> EnumerateNextPattern() {
            // 同じインデックスを固定しながら遷移を進める
            if (this.Index < _context.Count && this.Current < this.Previous) {
                long nextPoint = this.Point - _context.GourmetPoints[this.Current] + _context.GourmetPoints[this.Current + 1];
                yield return new CookiePattern(_context, nextPoint, this.Index, this.Previous, this.Current + 1);
            }

            // 次のインデックスの処理に遷移する
            if (this.Index + 1 < _context.Count && this.Current > 0) {
                long nextPoint = this.Point - _context.GourmetPoints[0] + _context.GourmetPoints[1];
                yield return new CookiePattern(_context, nextPoint, this.Index + 1, this.Current, 1);
            }
        }
    }

    #endregion

    #region 優先度付きキュー

    /// <summary>
    /// 優先度付きキュー
    /// </summary>
    internal class PriorityQueue<T> {

        /// <summary>
        /// 内部データ
        /// </summary>
        private readonly T[] _array;

        /// <summary>
        /// 比較方法
        /// </summary>
        private readonly IComparer<T> _comparer;

        /// <summary>
        /// 要素数
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PriorityQueue(int capacity, IComparer<T> comparer) {
            _array = new T[capacity];
            _comparer = comparer;
        }

        /// <summary>
        /// 要素を追加する
        /// </summary>
        public void Enqueue(T item) {
            // データの末尾に要素を追加する
            _array[this.Count] = item;
            this.Count++;

            // ヒープ木の再構築
            int child = this.Count - 1;
            while (child > 0) {
                int parent = (child - 1) / 2;
                if (_comparer.Compare(_array[parent], _array[child]) <= 0) break;
                (_array[parent], _array[child]) = (_array[child], _array[parent]);
                child = parent;
            }
        }

        /// <summary>
        /// 優先度に従い要素を取り出す
        /// </summary>
        public T Dequeue() {
            // 先頭の要素と末尾の要素を入れ替える
            (_array[0], _array[this.Count - 1]) = (_array[this.Count - 1], _array[0]);
            this.Count--;

            // ヒープ木の再構築
            int parent = 0;
            while (parent < this.Count / 2) {
                int left = parent * 2 + 1;
                int right = Math.Min(parent * 2 + 2, this.Count - 1);
                int child = _comparer.Compare(_array[left], _array[right]) > 0 ? right : left;
                if (_comparer.Compare(_array[parent], _array[child]) <= 0) break;
                (_array[parent], _array[child]) = (_array[child], _array[parent]);
                parent = child;
            }

            // 元々先頭にあった要素を返す
            return _array[this.Count];
        }
    }

    #endregion
}
