using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc442/tasks/abc442_d
 */

namespace D_SwapAndRangeSum {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            (_, int queriesCount) = (inputs[0], inputs[1]);

            // 区間和を求めるためのデータ構造の準備
            var structure = new SegmentTree(Console.ReadLine().Split(' ').Select(int.Parse));

            // 出力処理
            var answers = new List<long>();
            for (int i = 0; i < queriesCount; i++) {
                var queryInfo = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                if (queryInfo[0] == 1) {
                    structure.Swap(queryInfo[1] - 1);
                } else {
                    answers.Add(structure.GetRangeSum(queryInfo[1] - 1, queryInfo[2] - 1));
                }
            }
            Console.WriteLine(string.Join(Environment.NewLine, answers));
        }
    }

    #region 区間和の計算方法

    /// <summary>
    /// 区間和を求めるための構造
    /// </summary>
    interface IRangeSum {

        /// <summary>
        /// 指定したインデックスの要素と右隣の要素を入れ替える
        /// </summary>
        void Swap(int index);

        /// <summary>
        /// 区間和を求める
        /// </summary>
        long GetRangeSum(int leftIndex, int rightIndex);
    }

    /// <summary>
    /// 累積和
    /// </summary>
    internal class PrefixSum : IRangeSum {

        /// <summary>
        /// 数列
        /// </summary>
        private readonly int[] _numbers;

        /// <summary>
        /// 累積和
        /// </summary>
        private readonly long[] _prefixSum;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrefixSum(IEnumerable<int> numbers) {
            _numbers = numbers.ToArray();
            _prefixSum = new long[_numbers.Length + 1];
            for (int i = 0; i < _numbers.Length; i++) {
                _prefixSum[i + 1] = _prefixSum[i] + _numbers[i];
            }
        }

        /// <summary>
        /// 指定したインデックスの要素と右隣の要素を入れ替える
        /// </summary>
        public void Swap(int index) {
            if (index < 0 || _numbers.Length - 1 <= index) throw new ArgumentOutOfRangeException("配列外の要素との入れ替えは不可");

            // 累積和を更新してから要素を入れ替える
            _prefixSum[index + 1] = _prefixSum[index + 1] - _numbers[index] + _numbers[index + 1];
            (_numbers[index], _numbers[index + 1]) = (_numbers[index + 1], _numbers[index]);
        }

        /// <summary>
        /// 区間和を求める
        /// </summary>
        public long GetRangeSum(int left, int right) {
            if (right - left < 0 || left < 0 || _prefixSum.Length  - 1 <= right) throw new ArgumentException("区間指定が不正");
            return _prefixSum[right + 1] - _prefixSum[left];
        }
    }

    /// <summary>
    /// セグメント木
    /// </summary>
    internal class SegmentTree : IRangeSum {

        /// <summary>
        /// 葉の要素数（扱いやすいように 2 のべき乗を設定する）
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// ノード配列
        /// </summary>
        private readonly long[] _nodes;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SegmentTree(IEnumerable<int> numbers) {
            var leafs = numbers.ToArray();
            _size = 1;
            while (_size < leafs.Length) _size <<= 1;

            _nodes = new long[_size * 2];
            this.Initialize(leafs);
        }

        /// <summary>
        /// 葉の要素からセグメント木を初期化する
        /// </summary>
        private void Initialize(int[] leafs) {
            // 葉の初期化
            for (int i = 0; i < leafs.Length; i++) {
                _nodes[_size + i] = leafs[i];
            }

            // 葉以外の初期化
            for (int i = _size - 1; i > 0; i--) {
                _nodes[i] = _nodes[i * 2] + _nodes[i * 2 + 1];
            }
        }

        /// <summary>
        /// 指定したインデックスの要素と右隣の要素を入れ替える
        /// </summary>
        public void Swap(int index) {
            (int leftPos, long leftVal) = (_size + index, _nodes[_size + index]);
            (int rightPos, long rightVal) = (_size + index + 1, _nodes[_size + index + 1]);
            this.UpdateLeaf(leftPos, rightVal);
            this.UpdateLeaf(rightPos, leftVal);
        }

        /// <summary>
        /// インデックスで指定した葉の値を更新する
        /// </summary>
        private void UpdateLeaf(int index, long next) {
            _nodes[index] = next;
            index /= 2;
            while (index > 0) {
                _nodes[index] = _nodes[index * 2] + _nodes[index * 2 + 1];
                index /= 2;
            }
        }

        /// <summary>
        /// 区間和を求める
        /// </summary>
        public long GetRangeSum(int left, int right) {
            int leftIndex = left + _size;
            int rightIndex = right + _size;

            long result = 0;
            while (leftIndex <= rightIndex) {
                // leftIndex が右の子なら加算する
                if ((leftIndex & 1) == 1) {
                    result += _nodes[leftIndex];
                    leftIndex++;
                }

                // rightIndex が左の子なら加算する
                if ((rightIndex & 1) == 0) {
                    result += _nodes[rightIndex];
                    rightIndex--;
                }

                // 親へ移動する
                leftIndex >>= 1;
                rightIndex >>= 1;
            }
            return result;
        }
    }

    #endregion
}
