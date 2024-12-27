using System;
using System.Linq;
using System.Text;

/*
 <問題>
 いろはちゃんは長さ N (N ≥ 1) の正整数列 A = (A(1), A(2), …, A(N)) を持っています。
 いろはちゃんは A を使って、次のように文字列 S を生成しました。

 ・S = | から始める。
 ・i=1,2,…,N の順に、次の操作を行う。
   S の末尾に - を A(i) 個追加する。
   その後、 S の末尾に | を 1 個追加する。

 生成された文字列 S が与えられるので、正整数列 A を復元してください。

 <制約>
 ・S は問題文中の方法で生成された長さ 3 以上 100 以下の文字列
 ・A は長さ 1 以上の正整数列

 <入力>
 S

 <出力>
 答えを以下の形式で、1 行に半角空白区切りで出力せよ。
 A(1) A(2) … A(N)
 */

namespace B_Hurdle_Parsing {
    internal class Program {
        static void Main(string[] args) {

            var wSegmentLengths = Console.ReadLine().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length).ToArray();

            var wOutputLine = new StringBuilder();
            for (int i = 0; i < wSegmentLengths.Length; i++) {
                wOutputLine.Append(wSegmentLengths[i] + " ");
            }
            wOutputLine.Length--;
            Console.WriteLine(wOutputLine.ToString());
        }
    }
}
