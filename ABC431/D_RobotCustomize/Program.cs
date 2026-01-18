using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc431/tasks/abc431_d
 */

namespace D_RobotCustomize {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            long partsCount = long.Parse(Console.ReadLine());
            var parts = new List<Part>();
            for (long i = 0; i < partsCount; i++) {
                var inputs = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();
                long weight = inputs[0];
                long headHappiness = inputs[1];
                long bodyHappiness = inputs[2];
                parts.Add(new Part(weight, new Happiness(headHappiness, bodyHappiness)));
            }

            // ロボットの初期化
            var stragegy = new KnapsackHappinessStrategy();
            var robot = new Robot(parts, stragegy);
            robot.MaximizeHappiness();

            // 出力処理
            Console.WriteLine(robot.Happiness);
        }
    }

    #region ロボット

    internal class Robot {

        /// <summary>
        /// 部品リスト
        /// </summary>
        private readonly IReadOnlyList<Part> _parts = new List<Part>();

        /// <summary>
        /// 嬉しさの最大化戦略
        /// </summary>

        private readonly IHappinessStrategy _happinessStrategy;

        /// <summary>
        /// 嬉しさ
        /// </summary>
        public long Happiness { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Robot(IEnumerable<Part> parts, IHappinessStrategy stragegy) {
            _parts = parts.ToList();
            _happinessStrategy = stragegy;
        }

        /// <summary>
        /// 嬉しさを最大化する
        /// </summary>
        public void MaximizeHappiness() => this.Happiness = _happinessStrategy.CalcMax(_parts);
    }

    #endregion

    #region 部品

    /// <summary>
    /// 部品
    /// </summary>
    internal class Part {

        /// <summary>
        /// 重さ
        /// </summary>
        public long Weight { get; }

        /// <summary>
        /// 取り付けたときの嬉しさ
        /// </summary>
        public IHappiness Happiness { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Part(long weight, IHappiness happiness) {
            this.Weight = weight;
            this.Happiness = happiness;
        }
    }

    #endregion

    #region 嬉しさ関連

    /// <summary>
    /// 嬉しさインターフェース
    /// </summary>
    interface IHappiness {

        /// <summary>
        /// 頭に付けたときの嬉しさ
        /// </summary>
        long Head { get; }

        /// <summary>
        /// 体に付けたときの嬉しさ
        /// </summary>
        long Body { get; }
    }

    /// <summary>
    /// 嬉しさ
    /// </summary>
    internal class Happiness : IHappiness {

        /// <summary>
        /// 頭に付けたときの嬉しさ
        /// </summary>
        public long Head { get; }

        /// <summary>
        /// 体に付けたときの嬉しさ
        /// </summary>
        public long Body { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Happiness(long head, long body) {
            this.Head = head;
            this.Body = body;
        }
    }

    /// <summary>
    /// 嬉しさの最大化戦略インターフェース
    /// </summary>
    interface IHappinessStrategy {

        /// <summary>
        /// 嬉しさの最大値を計算する
        /// </summary>
        long CalcMax(IReadOnlyList<Part> parts);
    }

    /// <summary>
    /// 嬉しさの最大化戦略（ナップサック問題）
    /// </summary>
    internal class KnapsackHappinessStrategy : IHappinessStrategy {

        /// <summary>
        /// 嬉しさの最大値を計算する
        /// </summary>
        public long CalcMax(IReadOnlyList<Part> parts) {
            long maxHeadWeight = parts.Sum(part => part.Weight) / 2;

            // 動的計画法で嬉しさの差の最大値を求める
            var dp = new long[maxHeadWeight + 1];

            for (int i = 0; i < parts.Count; i++) {
                var part = parts[i];
                long happinessDiff = part.Happiness.Head - part.Happiness.Body;
                if (happinessDiff <= 0) continue;
                long partWeight = part.Weight;
                for (long w = maxHeadWeight; w >= partWeight; w--) {
                    dp[w] = Math.Max(dp[w], dp[w - partWeight] + happinessDiff);
                }
            }

            // 嬉しさの最大値 = すべて体に付けたときの嬉しさ + 嬉しさの差の最大値
            return parts.Sum(part => part.Happiness.Body) + dp.Max();
        }
    }

    #endregion
}
