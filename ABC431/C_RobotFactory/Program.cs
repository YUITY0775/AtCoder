using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc431/tasks/abc431_c
 */

namespace C_RobotFactory {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int headPartCount = inputs[0];
            int bodyPartCount = inputs[1];
            int neededRobots = inputs[2];
            var parts = new List<Part>();
            parts.AddRange(Console.ReadLine().Split(' ').Select((weight) => new HeadPart(int.Parse(weight))));
            parts.AddRange(Console.ReadLine().Split(' ').Select((weight) => new BodyPart(int.Parse(weight))));

            // ロボット工場の初期化
            var factory = new RobotFactory(new GreedyAssemblyStrategy());

            // 出力処理
            Console.WriteLine(factory.GetCreatableRobotCount(parts) < neededRobots ? "No" : "Yes");
        }
    }

    #region ロボット工場

    /// <summary>
    /// ロボット工場
    /// </summary>
    internal class RobotFactory {

        /// <summary>
        /// 組立戦略
        /// </summary>
        private readonly IRobotAssemblyStrategy _strategy;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RobotFactory(IRobotAssemblyStrategy strategy) {
            _strategy = strategy;
        }

        /// <summary>
        /// 組み立てられるロボットの数を取得する
        /// </summary>
        public int GetCreatableRobotCount(IEnumerable<Part> parts) => _strategy.Calculate(parts);
    }

    #endregion

    #region 部品関連

    /// <summary>
    /// 部品
    /// </summary>
    internal abstract class Part {

        /// <summary>
        /// 重さ
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Part(int weight) {
            this.Weight = weight;
        }
    }

    /// <summary>
    /// 頭部品
    /// </summary>
    internal class HeadPart : Part {
        public HeadPart(int weight) : base(weight) { }
    }

    /// <summary>
    /// 体部品
    /// </summary>
    internal class BodyPart : Part {
        public BodyPart(int weight) : base(weight) { }
    }

    #endregion

    #region 組立戦略

    /// <summary>
    /// ロボットの組立戦略
    /// </summary>
    interface IRobotAssemblyStrategy {
        /// <summary>
        /// 組み立てられるロボットの数を計算する
        /// </summary>
        int Calculate(IEnumerable<Part> parts);
    }

    /// <summary>
    /// 頭部品と体部品の重さを比較して、貪欲法でロボットを組み立てる戦略
    /// </summary>
    internal class GreedyAssemblyStrategy : IRobotAssemblyStrategy {

        /// <summary>
        /// 組み立てられるロボットの数を計算する
        /// </summary>
        /// <returns></returns>
        public int Calculate(IEnumerable<Part> parts) {
            var headParts = parts.OfType<HeadPart>().OrderByDescending(part => part.Weight).ToList();
            var bodyParts = parts.OfType<BodyPart>().OrderByDescending(part => part.Weight).ToList();

            int count = 0;
            int headPartIndex = 0;
            int bodyPartIndex = 0;
            while (headPartIndex < headParts.Count && bodyPartIndex < bodyParts.Count) {
                if (headParts[headPartIndex].Weight <= bodyParts[bodyPartIndex].Weight) {
                    count++;
                    bodyPartIndex++;
                }
                headPartIndex++;
            }
            return count;
        }
    }

    #endregion
}
