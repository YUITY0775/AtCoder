using System;

/*
 <問題>
 高橋君は歯が左右一列に N 本生えています。現在の高橋君の歯の状態はある文字列 S によって表されます。
 S の i 文字目が O のとき、左から i 番目の歯が丈夫であることを表します。
 S の i 文字目が X のとき、左から i 番目の歯が虫歯にかかっていることを表します。丈夫である歯は虫歯にかかっていません。
 高橋君はある連続する K 本の歯が丈夫であるとき、その K 本の歯を使ってイチゴを 1 個食べることができます。
 イチゴを食べると、その K 本の歯が虫歯にかかり丈夫でなくなります。
 このとき、高橋君は最大で何個のイチゴを食べることができるか求めてください。

 <制約>
 ・1 ≤ K ≤ N ≤ 100
 ・N, K は整数
 ・S は O と X からなる長さ N の文字列

 <入力>
 N K
 S

 <出力>
 答えを出力せよ。
 */

namespace B_Strawberries {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wLine = Console.ReadLine().Split(' ');
            (int wTeethCount, int wTeethForStrawberry) = (int.Parse(wLine[0]), int.Parse(wLine[1]));

            // 出力
            Console.WriteLine(CountStrawberries(wTeethForStrawberry, Console.ReadLine()));
        }

        /// <summary>
        /// 食べられるイチゴの数を求める。
        /// </summary>
        /// <param name="vTeethForStrawberry">歯の状態を示す文字列イチゴをひとつ食べるのに必要な歯の数</param>
        /// <param name="vTeethCondition">歯の状態を示す文字列</param>
        /// <returns>食べられるイチゴの数</returns>
        static int CountStrawberries(int vTeethForStrawberry, string vTeethCondition) {

            int wStrawberries = 0;
            int wStartIndex = 0;
            string wTeethForStrawberry = new string('O', vTeethForStrawberry);
            while (vTeethCondition.Length >= wStartIndex + vTeethForStrawberry) {
                if (vTeethCondition.Substring(wStartIndex, vTeethForStrawberry) == wTeethForStrawberry) {
                    wStrawberries++;
                    wStartIndex += vTeethForStrawberry;
                } else {
                    wStartIndex++;
                }
            }
            return wStrawberries;
        }
    }
}
