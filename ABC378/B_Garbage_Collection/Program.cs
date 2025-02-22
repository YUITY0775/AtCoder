using System;
using System.Linq;

/*
 <問題>
 AtCoder 市では、N 種類のゴミを定期的に収集しています。i( = 1, 2, …, N) 種類目のゴミは、日付を q(i) で割ったあまりが r(i)
 の日に収集されます。Q 個の質問に答えてください。
 j( = 1, 2, …, Q) 番目の質問では、d(j) 日に t(j) 種類目のゴミが出たときに、次にそれが収集される日を答えてください。 
 ただし、i 種類目のゴミが出た日が、i 種類目のゴミが回収される日であった場合、そのゴミは同じ日に収集されるとします。

 <制約>
 ・1 ≤ N ≤ 100
 ・0 ≤ r(i) < q(i) ≤ 10^9
 ・1 ≤ Q ≤ 100
 ・1 ≤ t(j) ≤ N
 ・1 ≤ d(j) ≤ 10^9
 ・入力はすべて整数

 <入力>
 N
 q(1) r(1)
 q(2) r(2)
 …
 q(N) r(N)
 Q
 t(1) d(1)
 t(2) d(2)
 …
 t(Q) d(Q)
​
 <出力>
 Q 行出力せよ。j(1≤j≤Q) 行目には、j 番目の質問に対する答えを出力せよ。
 */

namespace B_Garbage_Collection {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wGarbageKinds = int.Parse(Console.ReadLine());
            var wQueries = new (int Divisor, int Remainder)[wGarbageKinds];
            wQueries = ReceiveUserInput(wGarbageKinds);

            int wQuestionsCount = int.Parse(Console.ReadLine());
            var wQuestions = new (int Kind, int Day)[wQuestionsCount];
            wQuestions = ReceiveUserInput(wQuestionsCount);

            // 出力
            for (int i = 0; i < wQuestions.Length; i++) {
                Console.WriteLine(CalcGabageCollectionDay(wQuestions[i].Kind, wQuestions[i].Day, wQueries));
            }
        }

        /// <summary>
        /// ユーザー入力値をタプルの配列に変換する。
        /// </summary>
        /// <param name="vCount">ユーザーの入力行数</param>
        /// <returns>タプルの配列</returns>
        static (int, int)[] ReceiveUserInput(int vCount) {
            var wTupple = new (int, int)[vCount];
            for (int i = 0; i < vCount ; i++) {
                var wLine = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                wTupple[i] = (wLine[0], wLine[1]);
            }
            return wTupple;
        }

        /// <summary>
        /// ゴミが発生した日付とゴミの種類、ゴミの種類毎の計算式から、ゴミが収集される日付を返す。
        /// </summary>
        /// <param name="vDay">ゴミが発生した日付</param>
        /// <param name="vKind">ゴミの種類</param>
        /// <param name="vQueries">ゴミの種類毎の計算式</param>
        /// <returns></returns>
        static int CalcGabageCollectionDay(int vKind, int vDay, (int Divisor, int Remainder)[] vQueries) {
            int wMinIndex = 0;
            int wGabageCollectionDay = 0;
            while (vDay > wGabageCollectionDay) {
                wGabageCollectionDay = vQueries[vKind - 1].Divisor * wMinIndex + vQueries[vKind - 1].Remainder;
                wMinIndex++;
            }
            return wGabageCollectionDay;
        }
    }
}
