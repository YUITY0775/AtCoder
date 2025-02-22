using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 とある回転寿司に、1 から N までの番号が付けられた N 人の人が訪れています。人 i の 美食度 は A(i) です。
 今からベルトコンベア上を M 個の寿司が流れます。j 番目に流れる寿司の 美味しさ は B(j)です。

 それぞれの寿司は、人 1, 2, …, N の前をこの順に流れていきます。
 それぞれの人は、美味しさが自分の美食度以上である寿司が自分の前に流れてきたときはその寿司を取って食べ、それ以外のときは何もしません。
 人 i が取って食べた寿司は、人 j (j > i) の前にはもう流れてきません。

 M 個の寿司それぞれについて、その寿司を誰が食べるか、あるいは誰も食べないかどうかを求めてください。

 <制約>
 ・1 ≤ N, M ≤ 2×10^5
 ・1 ≤ A(i), B(i) ≤ 2×10^5 
 ・入力は全て整数

 <入力>
 N M
 A(1) A(2) … A(N)
 B(1) B(2) … B(M)

 <出力>
 M 行出力せよ。j (1 ≤ j ≤ M) 行目には、j 番目に流れる寿司を食べる人がいるならばその人の番号を、そうでないならば -1 を出力せよ。
 */

namespace C_Kaiten_Sushi {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            _ = Console.ReadLine();
            var wGourmetPoints = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var wTastePoints = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            // 美食度の配列を累積最小値の配列に変換
            var wCumulativeMinGourmetPoints = new int[wGourmetPoints.Length];
            wCumulativeMinGourmetPoints[0] = wGourmetPoints[0];
            for (int i = 1; i < wGourmetPoints.Length; i++) {
                wCumulativeMinGourmetPoints[i] = Math.Min(wCumulativeMinGourmetPoints[i - 1], wGourmetPoints[i]);
            }

            var wAnswers = Enumerable.Repeat(-1, wTastePoints.Length).ToArray();
            for (int i = 0; i < wTastePoints.Length; i++) {
                if (wTastePoints[i] < wCumulativeMinGourmetPoints[wCumulativeMinGourmetPoints.Length - 1]) continue;
                wAnswers[i] = BinarySearch(wCumulativeMinGourmetPoints, wTastePoints[i]);
            }

            // 出力
            Console.WriteLine(string.Join(Environment.NewLine, wAnswers));
        }

        /// <summary>
        /// 二分探索
        /// </summary>
        /// <param name="vSortedArray">降順でソートされたコレクション</param>
        /// <param name="vKey">検索値</param>
        /// <returns></returns>
        static int BinarySearch(IList<int> vSortedArray, int vKey) {
            int wResult = -1;
            int wLeftIndex = 0;
            int wRightIndex = vSortedArray.Count - 1;
            while (wLeftIndex <= wRightIndex) {
                int wMidIndex = (wRightIndex + wLeftIndex) / 2;
                if (vSortedArray[wMidIndex] <= vKey) {
                    wResult = wMidIndex + 1;
                    wRightIndex = wMidIndex - 1;
                } else {
                    wLeftIndex = wMidIndex + 1;
                }
            }
            return wResult;
        }
    }
}
