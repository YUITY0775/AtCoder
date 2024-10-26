using System;
using System.Collections.Generic;
using System.Text;

/*
 <問題>
 長さ N の文字列 S が与えられます。クエリが Q 個与えられるので、順番に処理してください。
 i 番目のクエリは以下の通りです。

 ・整数 X(i) と文字 C(i) が与えられるので、S の X(i) 番目の文字を C(i) に置き換える。
   その後、S に文字列 ABC が部分文字列として何箇所含まれるかを出力する。

 ここで、S の部分文字列とは、S の先頭から 0 文字以上、末尾から 0 文字以上削除して得られる文字列のことをいいます。
 例えば、ab は abc の部分文字列ですが、ac は abc の部分文字列ではありません。

 <制約>
 ・3 ≤ N ≤ 2×10^5
 ・1 ≤ Q ≤ 2×10^5 
 ・S は英大文字からなる長さ N の文字列
 ・1 ≤ X(i) ≤N
 ・C(i) は英大文字
​
 <入力>
 N Q
 S
 X(1) C(1)
 X(2) C(2)
 …
 X(Q) C(Q)

 <出力>
 Q 行出力せよ。i 行目 (1 ≤ i ≤ Q) には i 番目のクエリに対する答えを出力せよ。
 */

namespace C_Count_ABC_Again {
    internal class Program {

        static void Main(string[] args) {

            var wInput = Console.ReadLine().Split(' ');
            var wQueryCount = int.Parse(wInput[1]);

            var wText = new StringBuilder(Console.ReadLine());

            var wQueries = new List<Query>();
            for (int i = 0; i < wQueryCount; i++) {
                var wInputQuery = Console.ReadLine().Split(' ');
                wQueries.Add(new Query(int.Parse(wInputQuery[0]) - 1, char.Parse(wInputQuery[1])));
            }

            // 最初の1回は先頭から1文字ずつ探索
            var wCount = 0;
            for (int i = 0; i < wText.Length - 2; i++) {
                if (wText[i] == 'A' && wText[i + 1] == 'B' && wText[i + 2] == 'C') wCount++;
            }

            // 2回目以降は置換した文字の前後のみ探索
            foreach (var wQuery in wQueries) {
                // 同じ文字に変更するクエリはスキップ
                if (wText[wQuery.Index] != wQuery.Alphabet) {
                    wCount += CalcDiff(wText, wQuery);
                    wText[wQuery.Index] = wQuery.Alphabet;
                }
                    
                    
                Console.WriteLine(wCount);
            }
        }

        /// <summary>
        /// 文字列と文字置換の命令を受け取り、計測済の"ABC"の数をどれだけ更新すればよいかを返す
        /// </summary>
        /// <param name="vText">対象文字列</param>
        /// <param name="vQuery">命令</param>
        /// <returns>"ABC"の数を更新する数値</returns>
        static int CalcDiff(StringBuilder vText, Query vQuery) {

            int wCount = 0;

            switch (vQuery.Alphabet) {
                case 'A':
                    if (vQuery.Index < vText.Length - 2 && vText[vQuery.Index + 1] == 'B' && vText[vQuery.Index + 2] == 'C') {
                        wCount++;
                    }
                    break;
                case 'B':
                    if (0 < vQuery.Index && vQuery.Index < vText.Length - 1 && vText[vQuery.Index - 1] == 'A' && vText[vQuery.Index + 1] == 'C') {
                        wCount++;
                    }
                    break;
                case 'C':
                    if (1 < vQuery.Index && vText[vQuery.Index - 2] == 'A' && vText[vQuery.Index - 1] == 'B') {
                        wCount++;
                    }
                    break;
                default: break;
            }

            switch (vText[vQuery.Index]) {
                case 'A':
                    if (vQuery.Index < vText.Length - 2 && vText[vQuery.Index + 1] == 'B' && vText[vQuery.Index + 2] == 'C') {
                        wCount--;
                    }
                    break;
                case 'B':
                    if (0 < vQuery.Index && vQuery.Index < vText.Length - 1 && vText[vQuery.Index - 1] == 'A' && vText[vQuery.Index + 1] == 'C') {
                        wCount--;
                    }
                    break;
                case 'C':
                    if (1 < vQuery.Index && vText[vQuery.Index - 2] == 'A' && vText[vQuery.Index - 1] == 'B') {
                        wCount--;
                    }
                    break;
                default: break;
            }
            return wCount;
        }
    }

    internal class Query {
        public int Index { get; }
        public char Alphabet { get; }
        public Query(int vIndex, char vAlphabet) {
            this.Index = vIndex;
            this.Alphabet = vAlphabet;
        }
    }
}
