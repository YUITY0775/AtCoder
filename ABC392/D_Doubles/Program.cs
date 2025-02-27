using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 N 個のサイコロがあります。i 番目のサイコロは K(i) 個の面をもち、各面にはそれぞれ A(i, 1), A(i, 2), …, A(i, K(i)) が書かれています。
 このサイコロを振ると、各面がそれぞれ 1/K(i) の確率で出ます。
 あなたは N 個のサイコロから 2 個を選んで振ります。
 サイコロを適切に選んだときの、2 つのサイコロの出目が等しくなる確率の最大値を求めてください。

 <制約>
 ・2 ≤ N ≤ 100
 ・1 ≤ K(i) 
 ・K(1) + K(2) + … + K(N) ≤ 10^5
 ・1 ≤ A(i, j) ≤ 10^5
 ・入力は全て整数である

 <入力>
 N
 K(1) A(1, 1) A(1, 2) … A(1, K(1))
 …
 K(N) A(N, 1) A(N, 2) … A(N, K(N))

 <出力>
 答えを出力せよ。真の解との相対誤差または絶対誤差が 10^(−8) 以下のとき正解とみなされる。
 */

namespace D_Doubles {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wDiceCount = int.Parse(Console.ReadLine());
            var wDices = new Dice[wDiceCount];
            for (int i = 0; i < wDiceCount; i++) {
                var wDiceInfo = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                wDices[i] = new Dice(wDiceInfo[0], wDiceInfo.Skip(1).ToArray());
            }

            // 二つのサイコロについてすべての組み合わせをチェックする
            decimal wMaxProbability = 0;
            for (int i = 0; i < wDices.Length; i++) {
                for (int j = i + 1; j < wDices.Length; j++) {
                    wMaxProbability = Math.Max(wMaxProbability, wDices[i].CalcSameValueProbability(wDices[j]));
                }
            }

            // 出力
            Console.WriteLine(wMaxProbability);
        }
    }

    /// <summary>
    /// サイコロ
    /// </summary>
    internal class Dice {

        /// <summary>
        /// 面の数
        /// </summary>
        public int Sides { get; }

        /// <summary>
        /// サイコロの各面の目
        /// </summary>
        public int[] Values { get; }

        /// <summary>
        /// 同じ目が何面存在するかを格納した連想配列
        /// </summary>
        public Dictionary<int, int> ValueToCount { get; } = new Dictionary<int, int>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vSides">面の数</param>
        /// <param name="vValues">各面の目</param>
        public Dice(int vSides, int[] vValues) {
            this.Sides = vSides;
            this.Values = vValues;
            SetValueToCount();
        }

        /// <summary>
        /// サイコロの目から同じ目の面数を求める連想配列を構築する
        /// </summary>
        private void SetValueToCount() {
            foreach (int wValue in this.Values) {
                if (!this.ValueToCount.TryGetValue(wValue, out int wCount)) {
                    this.ValueToCount.Add(wValue, 1);
                    continue;
                }
                this.ValueToCount[wValue]++;
            }
        }

        /// <summary>
        /// 与えられたサイコロと合わせて二つのサイコロを振ったときに同じ目が出る確率を返す
        /// </summary>
        /// <param name="vDice">一緒に振るサイコロ</param>
        /// <returns>二つのサイコロを振った時に同じ目が出る確率</returns>
        public decimal CalcSameValueProbability(Dice vDice) {
            decimal wProbability = 0;
            foreach (int wUniqueValue in this.ValueToCount.Keys) {
                if (!vDice.ValueToCount.TryGetValue(wUniqueValue, out int wSameSidesCount)) continue;
                wProbability += ((decimal)this.ValueToCount[wUniqueValue] / this.Sides) * ((decimal)wSameSidesCount / vDice.Sides);
            }
            return wProbability;
        }
    }
}
