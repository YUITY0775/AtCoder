using System;
using System.Collections.Generic;

/*
 <問題>
 あなたはあるリングを両手で握っています。 このリングは N (N ≥ 3) 個のパーツ 1, 2, …, N によって構成されており、
 パーツ i とパーツ i + 1 (1 ≤ i ≤ N − 1)、およびパーツ 1 とパーツ N がそれぞれ隣接しています。
 最初、左手はパーツ 1 を、右手はパーツ 2 を握っています。 あなたは、1 回の 操作 で以下のことを行えます。

 ・片方の手を、今握っているパーツに隣接するいずれかのパーツに移動する。ただし、移動先にもう一方の手がない場合に限る。

 あなたは今から与えられる Q 個の指示に順番に従う必要があります。 
 i (1 ≤ i ≤ Q) 個目の指示は文字 H(i) および整数 T(i) によって表され、その意味は以下の通りです。

 ・操作を何回か（0 回でもよい）行うことで、H(i) が L ならば左手、R ならば右手が、パーツ T(i) を握っている状態にする。 
   このとき、H(i) によって指定された手ではない方の手を 動かしてはならない。

 <制約>
 ・3 ≤ N ≤ 100
 ・1 ≤ Q ≤ 100
 ・H(i) は L または R
 ・1 ≤ T(i) ≤ N
 ・N, Q, T(i) は整数 
 ・達成可能な指示しか与えられない

 <入力>
 N Q
 H(1) T(1)
 H(2) T(2)
 …
 H(Q) T(Q)

 <出力>
 すべての指示に従うために必要な操作回数の合計の最小値を出力せよ。
 */

namespace B_Hands_on_Rings_Easy {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wInputLine = Console.ReadLine().Split(' ');
            var wPartsCount = int.Parse(wInputLine[0]);
            var wQueriesCount = int.Parse(wInputLine[1]);

            var wQueries = new List<Query>();
            for (int i = 0; i < wQueriesCount; i++) {
                var wQueryInfo = Console.ReadLine().Split(' ');
                wQueries.Add(new Query(wQueryInfo[0], int.Parse(wQueryInfo[1])));
            }

            // 初期位置は左手が 1、右手が 2
            int wLeftHandPos = 1;
            int wRightHandPos = 2;
            int wTimesOfMovement = 0;

            foreach (var wQuery in wQueries) {

                // 移動先と現在の位置が同じであれば処理しない
                if (wQuery.Destination == wLeftHandPos || wQuery.Destination == wRightHandPos) continue;

                if (wQuery.Hand == "L") {
                    wTimesOfMovement += GetMinTimesOfMovement(wPartsCount, wLeftHandPos, wQuery.Destination, wRightHandPos);
                    wLeftHandPos = wQuery.Destination;
                } else {
                    wTimesOfMovement += GetMinTimesOfMovement(wPartsCount, wRightHandPos, wQuery.Destination, wLeftHandPos);
                    wRightHandPos = wQuery.Destination;
                }
            }
            Console.WriteLine(wTimesOfMovement);
        }

        /// <summary>
        /// 最小の移動回数を返す。
        /// </summary>
        /// <param name="vPartsCount">パーツ数</param>
        /// <param name="vHandPos">移動対象の手の位置</param>
        /// <param name="vDestination">移動先</param>
        /// <param name="vOtherHandPos">移動対象ではない手の位置</param>
        /// <returns>最小の移動回数</returns>
        static int GetMinTimesOfMovement(int vPartsCount, int vHandPos, int vDestination, int vOtherHandPos) {

            // 移動前の位置と移動先の位置を入れ替えても移動回数に影響はない
            // 大小比較を簡易にするため、常に vHandPos < vDestination が成り立つようにする
            if (vDestination < vHandPos) {
                int wTemp = vDestination;
                vDestination = vHandPos;
                vHandPos = wTemp;
            }

            if (vHandPos < vOtherHandPos && vOtherHandPos < vDestination) {
                return vPartsCount - vDestination + vHandPos;
            } else {
                return vDestination - vHandPos;
            }
        }
    }

    internal class Query {
        public string Hand { get; }
        public int Destination { get; }
        public Query(string vHand, int vDestination) {
            this.Hand = vHand;
            this.Destination = vDestination;
        }
    }
}
