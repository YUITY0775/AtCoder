using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 AtCoder では、ARC が 2 つの division に分けられています。

 ARC Div.1 では、コンテスト開始時のレーティングが 1600 以上 2799 以下の参加者がレーティング更新の対象です。
 ARC Div.2 では、コンテスト開始時のレーティングが 1200 以上 2399 以下の参加者がレーティング更新の対象です。

 高橋くんは、これから N 回の ARC に参加することにしました。
 はじめ、高橋くんのレーティングは R です。

 i 回目 (1 ≤ i ≤ N) の ARC は Div.D(i) で、高橋くんが取った成績は整数 A(i) で表されます。
 i 回目の ARC において高橋くんがレーティング更新の対象ならば、コンテスト開始時の高橋くんのレーティングを 
 T として、更新後の高橋くんのレーティングは T + A(i) になります。
 高橋くんがレーティング更新の対象でなければ、高橋くんのレーティングは変化しません。
 ARC でのレーティングの更新はコンテストが終了したあと直ちに行われ、次のコンテストのレーティング更新の対象であるかは
 更新後のレーティングをもとに判定されます。

 N 回の ARC を終えたとき、高橋くんのレーティングがいくつになっているか求めてください。
 ただし、高橋くんはこの N 回の ARC 以外のコンテストには参加せず、ARC 以外でレーティングが変動することはありません。

 <制約>
 ・1 ≤ N ≤ 100
 ・0 ≤ R ≤ 4229
 ・1 ≤ D(i) ≤ 2 (1 ≤ i ≤ N)
 ・−1000 ≤ A(i) ≤ 1000 (1 ≤ i ≤ N)
 ・入力はすべて整数

 <入力>
 N R
 D(1) A(1)
 D(2) A(2)
 …
 D(N) A(N)

 <出力>
 N 回の ARC を終えた後の、高橋くんのレーティングを出力せよ。
 */

namespace B_ARC_Division {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wLineInfo = Console.ReadLine().Split(' ').ToArray();
            int wContestCount = int.Parse(wLineInfo[0]);
            int wRating = int.Parse(wLineInfo[1]);

            var wContestResult = new List<(bool IsDivFirst, int Result)>();
            for (int i = 0; i < wContestCount; i++) {
                var wLine = Console.ReadLine().Split(' ').ToArray();
                wContestResult.Add((Contest.IsDivFirst(int.Parse(wLine[0])), int.Parse(wLine[1])));
            }

            for (int i = 0; i < wContestCount; i++) {
                if (Contest.IsRatingTarget(wContestResult[i].IsDivFirst, wRating)) {
                    wRating = (wContestResult[i].Result + wRating) < 0 ? 0 : wContestResult[i].Result + wRating;  
                }
            }

            // 出力
            Console.WriteLine(wRating);
        }
    }

    /// <summary>
    /// コンテストクラス
    /// </summary>
    internal static class Contest {

        /// <summary>
        /// コンテスト種類定義
        /// </summary>
        private static readonly int C_DivFirst = 1;
        
        /// <summary>
        /// コンテスト種類が Div.1 に該当するか否かを判定する
        /// </summary>
        /// <param name="vContestNumber">コンテスト種類</param>
        public static bool IsDivFirst(int vContestNumber) => vContestNumber == C_DivFirst;

        /// <summary>
        /// コンテストのレーティング対象か否かを判定する
        /// </summary>
        /// <param name="vIsDivFirst">コンテスト種類が Div.1 に該当するか否か</param>
        /// <param name="vCurrentRating">現在のレーティング</param>
        public static bool IsRatingTarget(bool vIsDivFirst, int vCurrentRating) {
            if (vIsDivFirst) return 1600 <= vCurrentRating && vCurrentRating <= 2799;
            return 1200 <= vCurrentRating && vCurrentRating <= 2399; 
        }
    }
}
