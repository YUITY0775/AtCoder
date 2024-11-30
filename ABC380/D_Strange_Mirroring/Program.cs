using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 <問題>
 英大小文字からなる文字列 S が与えられます。
 S に以下の操作を 10^100 回繰り返します。

 ・まず、S の大文字を小文字に、小文字を大文字に書き換えた文字列を T とする。
 ・その後、S と T とをこの順に連結した文字列を新たな S とする。

 Q 個の質問に答えて下さい。 そのうち i 個目は次の通りです。

 ・全ての操作を終えた後の S の先頭から K(i) 文字目を求めよ。

 <制約>
 ・S は英大小文字からなる長さ 1 以上 2×10^5 以下の文字列
 ・Q, K(i) は整数
 ・1 ≤ Q ≤ 2×10^5  
 ・1 ≤ K(i) ≤ 10^18

 <入力>
 S Q
 K(1) K(2) … K(Q)
​
 <出力>
 i 個目の質問の答えを C(i) とする時、以下の形式で 1 行に空白区切りで出力せよ。
 C(1) C(2) … C(Q)
 */

namespace D_Strange_Mirroring {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            string wAlphabets = Console.ReadLine();
            int wQueryCount = int.Parse(Console.ReadLine());
            var wQueries = Console.ReadLine().Split(' ').Select(x => long.Parse(x));

            // 出力
            var wOutputLine = new StringBuilder();
            foreach (var wChar in ExecuteQueries(wAlphabets, wQueries)) {
                wOutputLine.Append(wChar + " ");
            }
            wOutputLine.Length--;
            Console.WriteLine(wOutputLine.ToString());
        }

        /// <summary>
        /// 元の文字列とクエリを受け取り、クエリ実行結果を返す。
        /// </summary>
        /// <param name="vOriginal">元の文字列</param>
        /// <param name="vQueries">クエリ</param>
        /// <returns>クエリ実行により得られた文字</returns>
        static IEnumerable<char> ExecuteQueries(string vOriginal, IEnumerable<long> vQueries) {

            foreach (long wQuery in vQueries) {

                // 指数
                int wIndex = 0;
                while ((wQuery / vOriginal.Length) > Math.Pow(2, wIndex)) {
                    wIndex++;
                }

                long wSegmentOrder = 0;
                int wAlphabetOrder = 0;
                if (wQuery % vOriginal.Length == 0) {
                    wSegmentOrder = wQuery / vOriginal.Length - 1;
                    wAlphabetOrder = vOriginal.Length - 1;
                } else {
                    wSegmentOrder = wQuery / vOriginal.Length;
                    wAlphabetOrder = (int)(wQuery % vOriginal.Length - 1);
                }

                yield return IsObverse(wSegmentOrder, wIndex) ? vOriginal[wAlphabetOrder] : GetInvertedChar(vOriginal[wAlphabetOrder]);
            }
        }

        /// <summary>
        /// 大文字 ⇔ 小文字 反転前の文字列であるか否かを返す。
        /// </summary>
        /// <param name="vSegmentOrder">文字列のまとまりの順番</param>
        /// <param name="vIndex"></param>
        /// <returns>反転前の文字列である場合true, そうではない場合false</returns>
        static bool IsObverse(long vSegmentOrder, int vIndex) {
            int wCount = 0;
            while (vIndex >= 0) {
                if (vSegmentOrder == 0) return wCount % 2 == 0;
                if (vSegmentOrder == 1) break;
                vSegmentOrder -= (long)Math.Pow(2, vIndex);
                vIndex--;
                wCount++;
            }
            return wCount % 2 == 1;
        }

        /// <summary>
        /// アルファベットの 小文字 ⇔ 大文字 変換を行う
        /// </summary>
        /// <param name="vAlphabet">変換対象のアルファベット</param>
        /// <returns>変換後のアルファベット</returns>
        static char GetInvertedChar(char vAlphabet) => char.IsUpper(vAlphabet) ? char.ToLower(vAlphabet) : char.ToUpper(vAlphabet);
    }
}
