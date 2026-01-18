using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc431/tasks/abc431_b
 */

namespace B_RobotWeight {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int initialWeight = int.Parse(Console.ReadLine());
            int partCount = int.Parse(Console.ReadLine());
            var idToPart = Console.ReadLine().Split(' ')
                .Select((weight, index) => new Part(index, int.Parse(weight)))
                .ToDictionary(part => part.Id, part => part);
            int queryCount = int.Parse(Console.ReadLine());

            // ロボットの初期化
            var robot = new Robot(initialWeight);

            // 出力処理
            var answers = new List<int>();
            for (int i = 0; i < queryCount; i++) {
                var partId = int.Parse(Console.ReadLine()) - 1;
                robot.AttachOrDetachPart(idToPart[partId]);
                answers.Add(robot.Weight);
            }
            Console.WriteLine(string.Join(Environment.NewLine, answers));
        }
    }

    #region ロボット関連

    /// <summary>
    /// ロボット
    /// </summary>
    internal class Robot {

        /// <summary>
        /// 各部品が取り付けられているかどうか
        /// </summary>
        private readonly HashSet<Part> _attachedParts = new HashSet<Part>();

        /// <summary>
        /// 重さ
        /// </summary>
        public int Weight { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Robot(int weight) {
            this.Weight = weight;
        }

        /// <summary>
        /// 部品の取り付け・取り外し
        /// </summary>
        public void AttachOrDetachPart(Part part) {
            if (_attachedParts.Add(part)) {
                this.Weight += part.Weight;
            } else {
                _attachedParts.Remove(part);
                this.Weight -= part.Weight;
            }
        }
    }

    /// <summary>
    /// 部品
    /// </summary>
    internal class Part {

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 重さ
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Part(int id, int weight) {
            this.Id = id;
            this.Weight = weight;
        }

        /// <summary>
        /// IDが同じなら同一の部品とみなすためのオーバーライド
        /// </summary>
        public override bool Equals(object obj) => obj is Part part && this.Id == part.Id;
        public override int GetHashCode() => Id.GetHashCode();
    }

    #endregion
}
